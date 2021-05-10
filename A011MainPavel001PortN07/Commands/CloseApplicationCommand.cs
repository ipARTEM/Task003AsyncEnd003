using Task003AsyncEnd003.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task003AsyncEnd003.Commands
{
    // Чтобы можно было до него достучаться из разметки надо 1 - установить пространство имен команды 
    //                                                     и 2- написать развёрнутую команду в XAML                       
    public class CloseApplicationCommand : Command
    {
        // Перенаправленный от абстрактного
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
