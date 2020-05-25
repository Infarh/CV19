using System;
using CV19.Infrastructure.Commands.Base;

namespace CV19.Infrastructure.Commands
{
    /// <summary>
    /// Выполнение подключенной лямбда-команды
    /// </summary>
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        /// <summary>
        /// Подключение лямбда-команды
        /// </summary>
        /// <param name="Execute">Выполняемый код</param>
        /// <param name="CanExecute">Возможность выполнения</param>
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => _Execute(parameter);
    }
}
