using EF_Core_S04.AppDbContexts;
using EF_Core_S04.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_S04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using NationalBankDbContext DbContext = new NationalBankDbContext();

            #region Interactive Menu
            #region Add a new Customer
            static void AddCustomer(NationalBankDbContext dbContext)
            {
                var customer = new Customer();
                Console.WriteLine("--- Add New Customer ---");
                Console.Write("Full Name: ");
                customer.FullName = Console.ReadLine();
                Console.Write("National ID: ");
                customer.NationalId = Console.ReadLine();
                Console.Write("Date of Birth (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime DOB))
                {
                    customer.DateOfBirth = DOB;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                    return;
                }
                Console.Write("Email: ");
                customer.Email = Console.ReadLine();
                Console.Write("Phone: ");
                customer.PhoneNumber = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Customer Type: \n\t1)Individual \n\t2)Corporate) \n  Choice: ");
                customer.CustomerType = Console.ReadLine() == "1" ? "Individual" : "Corporate";

                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();

                Console.WriteLine($"Customer created successfully. CustomerId = {customer.Id}");
            }
            #endregion

            #region Open a new Account for a Customer
            static void OpenNewAccount(NationalBankDbContext dbContext)
            {
                Console.Write("Account number: ");
                var accountNumber = Console.ReadLine()!;
                if (dbContext.Accounts.Any(A => A.AccountNumber == accountNumber))
                {
                    Console.WriteLine("An account with this number already exists.");
                    return;
                }

                Console.Write("Account Type: \n\t1)Savings \n\t2)Current \n\t3)Business \n  Choice: ");
                var typeChoice = Console.ReadLine();
                string accountType;
                switch (typeChoice)
                {
                    case "1": accountType = "Savings"; break;
                    case "2": accountType = "Current"; break;
                    case "3": accountType = "Business"; break;
                    default:
                        Console.WriteLine("Invalid account type.");
                        return;
                }

                Console.Write("Branch code: ");
                var branchCode = Console.ReadLine()!;
                Branch branch = dbContext.Branches.FirstOrDefault(B => B.Code == branchCode);
                if (branch != null)
                {
                    Console.WriteLine("Branch not found.");
                    return;
                }

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out var CustomerId))
                {
                    Console.WriteLine("Invalid customer Id.");
                    return;
                }
                Customer Customer = dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId);
                if (Customer != null)
                {
                    Console.WriteLine("Customer not found.");
                    return;
                }

                Console.WriteLine("Ownership role: \n\t1) Primary  \n\t2) CoHolder \n  Choose:");
                var roleChoice = Console.ReadLine();
                string ownershipType;
                switch (roleChoice)
                {
                    case "1": ownershipType = "Primary"; break;
                    case "2": ownershipType = "CoHolder"; break;
                    default:
                        Console.WriteLine("Invalid ownership role.");
                        return;
                }

                var account = new Account
                {
                    AccountNumber = accountNumber,
                    AccountType = accountType,
                    branchCode = branch.Code,
                    CurrentBalance = 0,
                    OpeningDate = DateTime.Now
                };

                var accountCustomer = new AccountCustomers
                {
                    AccountNumber = account.AccountNumber,
                    CustomerId = Customer.Id,
                    OwnerShipType = ownershipType,
                    OwnerShipStartDate = DateTime.Now,
                    AccountStatus = "Active"
                };

                dbContext.Accounts.Add(account);
                dbContext.AccountCustomers.Add(accountCustomer);
                dbContext.SaveChanges();
                Console.WriteLine($"Account '{accountNumber}' created and linked to customer {CustomerId} as {ownershipType} owner.");
            }
            #endregion

            #region Update Account Status
            static void UpdateAccountStatus(NationalBankDbContext dbContext)
            {
                Console.WriteLine("--- Update Account Status ---");
                Console.Write("Account Number: ");
                var account = Console.ReadLine();
                if (dbContext.Accounts.Any(A => A.AccountNumber != account))
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out var customerId))
                {
                    Console.WriteLine("Invalid customer Id.");
                    return;
                }

                AccountCustomers accountCustomers = dbContext.AccountCustomers.FirstOrDefault(AC => AC.AccountNumber == account && AC.CustomerId == customerId);
                if (accountCustomers == null)
                {
                    Console.WriteLine("Account-Customer relationship not found.");
                    return;
                }

                Console.Write("New Status: \n\t1) Active \n\t2) Closed \n  Choice: ");
                var newStatus = Console.ReadLine();
                switch (newStatus)
                {
                    case "1": accountCustomers.AccountStatus = "Active"; break;
                    case "2": accountCustomers.AccountStatus = "Closed"; break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }

                dbContext.SaveChanges();

                Console.WriteLine("Status updated to Closed.");
            }
            #endregion

            #region Remove an Account from a Customer
            static void RemoveAccountFromCustomer(NationalBankDbContext dbContext)
            {
                Console.WriteLine("--- Remove Account From Customer ---");
                Console.Write("Account number: ");
                var account= Console.ReadLine()!;
                if (dbContext.Accounts.Any(A => A.AccountNumber != account))
                {
                    Console.WriteLine("Account not found");
                    return;
                }

                Console.Write("Customer Id: ");
                if (!int.TryParse(Console.ReadLine(), out var customerId))
                {
                    Console.WriteLine("Invalid customer Id.");
                    return;
                }

                AccountCustomers accountCustomers = dbContext.AccountCustomers.FirstOrDefault(AC => AC.AccountNumber == account && AC.CustomerId == customerId);
                if (accountCustomers == null)
                {
                    Console.WriteLine("Account-Customer relationship not found.");
                    return;
                }

                dbContext.AccountCustomers.Remove(accountCustomers);
                dbContext.SaveChanges();

                Console.WriteLine("Ownership link deleted.");
                Console.WriteLine($"That was the last owner - account '{account}' was also removed.");
            }
            #endregion

            #region List all Customers (with accounts)
            static void ListAllCustomers(NationalBankDbContext dbContext)
            {
                Console.WriteLine(" All Customers ---");
                var customers = dbContext.Customers.Include(C => C.accountCustomers)
                                                   .ThenInclude(AC => AC.account)
                                                   .ThenInclude(A => A.branch)
                                                   .OrderBy(C => C.Id)
                                                   .ToList();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    return;
                }

                foreach (var customer in customers)
                {
                    Console.WriteLine($"  #{customer.Id} {customer.FullName} ({customer.CustomerType})");

                    if (customer.accountCustomers.Count == 0)
                    {
                        Console.WriteLine("\t(no accounts)");
                        continue;
                    }

                    foreach (var AC in customer.accountCustomers)
                    {
                        Console.WriteLine($"\t{AC.account.AccountNumber,-10}{AC.account.AccountType,-9} Balance: {AC.account.CurrentBalance,10:N2}   {AC.OwnerShipType,-8} {AC.AccountStatus,-7} @ {AC.account.branch.Name}");
                    }
                }
            }
            #endregion

            #region Menu loop
            while (true)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("    National Bank - Management");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1) Add a new Customer");
                Console.WriteLine(" 2) Open a new Account for a Customer");
                Console.WriteLine(" 3) Update Account Status");
                Console.WriteLine(" 4) Remove an Account from a Customer");
                Console.WriteLine(" 5) List all Customers (with accounts)");
                Console.WriteLine(" 0) Exit");
                Console.WriteLine("------------------------------------------");
                Console.Write("    Enter Choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddCustomer(DbContext);
                        break;
                    case "2":
                        OpenNewAccount(DbContext);
                        break;
                    case "3":
                        UpdateAccountStatus(DbContext);
                        break;
                    case "4":
                        RemoveAccountFromCustomer(DbContext);
                        break;
                    case "5":
                        ListAllCustomers(DbContext);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            #endregion

            #endregion
        }
    }
}
