namespace BankApplication
{
    public abstract class Account
    {
        protected decimal Balance { get; set; }
        public string HolderName { get; set; }

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

        public abstract string Withdraw(decimal amount);

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
        private Dictionary<string, Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new Dictionary<string, Account>();
        }

        public void AddAccount(string accountId, Account account)
        {
            Accounts[accountId] = account;
        }

        public string ProcessTransaction(string accountId, string action, decimal amount)
        {
            if (!Accounts.ContainsKey(accountId))
                return "Account not found";

            Account account = Accounts[accountId];
            switch (action.ToLower())
            {
                case "deposit":
                    return account.Deposit(amount);
                case "withdraw":
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

            bank.AddAccount("SAVING ACCOUNT", savings);
            bank.AddAccount("CURRENT ACCOUNT", current);

            Console.WriteLine(bank.ProcessTransaction("SAVING ACCOUNT", "withdraw", 600));
            Console.WriteLine(bank.ProcessTransaction("CURRENT ACCOUNT", "withdraw", 1200));

            bank.DisplayAccounts();
        }
    }
}
