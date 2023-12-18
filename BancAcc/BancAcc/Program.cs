namespace BankAccount
{
    class Account
    {
        private decimal balance; // Поле для хранения баланса счета

        public Account(decimal initialBalance)
        {
            balance = initialBalance; // Инициализация поля баланса счета значением initialBalance
        }

        public decimal Balance
        {
            get { return balance; } // Свойство для доступа к текущему балансу счета (только для чтения)
        }

        public void Deposit(decimal amount)
        {
            // Имитация задержки в операции пополнения
            Thread.Sleep(1000);
            balance += amount; // Увеличение баланса на указанную сумму
            Console.WriteLine($"Пополнение на {amount} руб. Баланс: {balance} руб.");
        }

        public void Withdraw(decimal amount)
        {
            if (balance >= amount)
            {
                balance -= amount; // Уменьшение баланса на указанную сумму
                Console.WriteLine($"Снятие {amount} руб. Баланс: {balance} руб.");
            }
            else
            {
                Console.WriteLine("Недостаточно средств на счете."); // Вывод сообщения об ошибке, если недостаточно средств на счете
            }
        }

        public void WaitForBalance(decimal targetAmount)
        {
            while (balance < targetAmount)
            {
                // Пауза в ожидании пополнения счета
                Thread.Sleep(1000);
            }

            Console.WriteLine($"Баланс достиг {targetAmount} руб., можно снять деньги.");
        }
    }

    class Program
    {
        static void Main(string args)
        {
            Account account = new Account(1000m); // Создание объекта класса Account с начальным балансом 1000 руб.

            // Запуск отдельного потока для пополнения счета
            Thread depositThread = new Thread(() => DepositRandomAmount(account));
            depositThread.Start();

            // Ожидание пополнения счета до 5000 руб.
            account.WaitForBalance(5000m);

            // Снятие 3000 руб. со счета
            account.Withdraw(3000m);

            // Вывод остатка на балансе
            Console.WriteLine($"Остаток на балансе: {account.Balance} руб.");

            // Ожидание завершения работы потока пополнения
            depositThread.Join();
        }

        static void DepositRandomAmount(Account account)
        {
            Random random = new Random();
            while (true)
            {
                decimal amount = random.Next(100, 1000);
                account.Deposit(amount);
            }
        }
    }
}
