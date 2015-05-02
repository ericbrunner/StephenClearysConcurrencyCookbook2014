using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExceptionHandlingOnAsyncVoid
{
    /// <summary>
    /// Samples from chapter 2.9.
    /// </summary>
    public static class ExceptionHandlingOnAsyncVoid 
    {
        public class MyAsyncCommand : ICommand
        {

            bool ICommand.CanExecute(object parameter)
            {
                throw new NotImplementedException();
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { throw new NotImplementedException(); }
                remove { throw new NotImplementedException(); }
            }


            /// <summary>
            /// Defines the "async" method to be called when the command is invoked.
            /// </summary>
            /// <param name="parameter">Data used by the command.  
            /// If the command does not require data to be passed, this object 
            /// can be set to null.</param>
            async void ICommand.Execute(object parameter)
            {
                await this.InnerExecute(parameter);
            }

            /// <summary>
            /// Defines the "inner" async method to be called when the command is invoked.
            /// </summary>
            /// <param name="parameter">Data used by the command.
            /// If the command does not require data to be passed, this object
            /// can be set to null.</param>
            /// <returns></returns>
            /// <exception cref="System.InvalidOperationException">parameter is invalid.</exception>
            internal async Task InnerExecute(object parameter)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                System.Diagnostics.Debug.WriteLine(
                    string.Format("Execute parameter: '{0}'", parameter));

                throw new InvalidOperationException("parameter is invalid.");
            }

            /// <summary>
            /// The methods exception gets only caught with the AsyncContext (Nito.AsyncEx nuget package)
            /// </summary>
            /// <exception cref="System.InvalidOperationException">parameter is invalid.</exception>
            public async void ExecuteWithoutTask(object parameter)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                System.Diagnostics.Debug.WriteLine(
                        string.Format("Execute parameter: '{0}'", parameter));

                throw new InvalidOperationException("parameter is invalid.");
            }

        }
    }
}
