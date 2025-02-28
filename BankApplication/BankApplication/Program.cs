namespace BankApplication
{
    public enum TransactionType
    {
        Deposit,
        Withdraw
    }

    public enum AccountType
    {
        Current,
        Saving
    }
    public abstract class Account
    {
        protected decimal Balance { get; set; }
        public string HolderName { get; set; }
        public abstract string Withdraw(decimal amount);

        public Account(string holderName,decimal initialBalance = 0)
        {
            HolderName = holderName;
            Balance = initialBalance;
        }

        public string Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return $"Deposited Rs {amount}. New balance: Rs {Balance}";
            }
            return "Invalid deposit amount";
        }

        public decimal GetBalance()
        {
            return Balance;
        }
    }

    public class SavingsAccount : Account
    {
        private decimal WithdrawalLimit { get; set; }

        public SavingsAccount(string holderName,decimal initialBalance = 0, decimal withdrawalLimit = 1000)
            : base(holderName,initialBalance)
        {
            WithdrawalLimit = withdrawalLimit;
        }

        public override string Withdraw(decimal amount)
        {
            if (amount <= 0)
                return "Invalid withdrawal amount";
            if (amount > WithdrawalLimit)
                return $"Withdrawal exceeds limit of Rs {WithdrawalLimit}";
            if (amount <= Balance)
            {
                Balance -= amount;
                return $"Withdrew Rs {amount}. New balance: Rs {Balance}";
            }
            return "Insufficient funds";
        }
    }

    public class CurrentAccount : Account
    {
        private decimal AccountLimitExceeded { get; set; }

        public CurrentAccount(string holderName,decimal initialBalance = 0, decimal accountLimitExceeded = 500)
            : base(holderName, initialBalance)
        {
            AccountLimitExceeded = accountLimitExceeded;
        }

        public override string Withdraw(decimal amount)
        {
            if (amount <= 0)
                return "Invalid withdrawal amount";
            if (amount <= (Balance + AccountLimitExceeded))
            {
                Balance -= amount;
                return $"Withdraw Rs {amount}. New balance: Rs {Balance}";
            }
            return "Exceeds bank amount limit";
        }
    }


    public class Bank
    {
        private Dictionary<AccountType, Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new Dictionary<AccountType, Account>();
        }

        public void AddAccount(AccountType accountType, Account account)
        {
            Accounts[accountType] = account;
        }

        public string ProcessTransaction(AccountType accountType, TransactionType action, decimal amount)
        {
            if (!Accounts.ContainsKey(accountType))
                return "Account not found";

            Account account = Accounts[accountType];
            switch (action)
            {
                case TransactionType.Deposit:
                    return account.Deposit(amount);
                case TransactionType.Withdraw:
                    return account.Withdraw(amount);
                default:
                    return "Invalid action";
            }
        }

            public void DisplayAccounts()
            {
            foreach (var account in Accounts)
              {
                Console.WriteLine($"Account Type : {account.Key}," +
                    $" HolderName : {(account.Value).HolderName}," +
                    $" Bank Balance : {(account.Value).GetBalance()}");
               }
        }
        
    }

    class Program
    {
        static void Main()
        {
            Bank bank = new Bank();
            SavingsAccount savings = new SavingsAccount("ravi",500, 1000);
            CurrentAccount current = new CurrentAccount("mohan",1000, 500);

            bank.AddAccount(AccountType.Saving, savings);
            bank.AddAccount(AccountType.Current, current);

            Console.WriteLine(bank.ProcessTransaction(AccountType.Saving, TransactionType.Withdraw, 600));
            Console.WriteLine(bank.ProcessTransaction(AccountType.Current, TransactionType.Withdraw, 1200));

            bank.DisplayAccounts();
        }
    }
}
