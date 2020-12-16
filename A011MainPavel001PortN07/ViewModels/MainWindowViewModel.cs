using A011MainPavel001PortN07.Commands;
using A011MainPavel001PortN07.ViewModels.Base;
using Microsoft.Win32;
using QuikSharp.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace A011MainPavel001PortN07.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region _SelectedPageIndex :int - Номер выбранной вкладки
        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        private int _SelectedPageIndex;

        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region Заголовок окна
        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _Title = "MoneyMagnet MVVM";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;

            set => Set(ref _Title, value);
        }

        #endregion

        #region Status:string -Статус программы

        /// <summary>
        /// Статус программы
        /// </summary>
        private string _Status = "Готов!";

        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion



        /************************************************************************************************************/

        #region Команды

        #region CLoseApplicationCommand

        public ICommand CloseApplicationCommand2 { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region ChangeTabIndexCommand           
        //public ICommand ChangeTabIndexCommand { get; }
        //public ObservableCollection<Group> Groups { get; }
        //public object[] CompositeCollection { get; }

        //private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        //private void OnChangeTabIndexCommandExecuted(object p)
        //{
        //    //if (!(p is int count)) return;
        //    //SelectedPageIndex += count;

        //    // или 
        //    if ((p is null)) return;
        //    {
        //        SelectedPageIndex += Convert.ToInt32(p);
        //    }
        //}

        #endregion

        #region CreateGroupCommand
        //public ICommand CreateGroupCommand { get; }

        //private bool CanCreateGroupCommandExecute(object p) => true;

        //private void OnCreateGroupCommandExecuted(object p)
        //{
        //    var group_max_index = Groups.Count + 1;

        //    var new_group = new Group
        //    {
        //        Name = $"Группа {group_max_index}",
        //        Students = new ObservableCollection<Student>()

        //    };

        //    Groups.Add(new_group);

        //}

        #endregion

        #region DeleteGroupCommand

        //public ICommand DeleteGroupCommand { get; }

        //private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        //private void OnDeleteGroupCommandExecuted(object p)
        //{
        //    if (!(p is Group group)) return;

        //    var group_index = Groups.IndexOf(group);

        //    Groups.Remove(group);
        //    if (group_index < Groups.Count)
        //        SelectedGroup = Groups[group_index];
        //}

        #endregion


        ClassAllTrade classAllTrade = new ClassAllTrade();
        ObservableCollection<ClassAllTrade> CollClassAllTrade = new ObservableCollection<ClassAllTrade>();

        public ICommand AllTradeCommand { get; }

        private bool CanAllTradeCommandExecute(object p) => p is AllTrade allTrade;

        private void OnAllTradeCommandExecuted(AllTrade allTrade)
        {




            if (allTrade.SecCode == "RIZ0")
            {


                classAllTrade.Name = allTrade.SecCode;
                classAllTrade.TimeSave = DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss.ffff");

                string dateAllTrade = allTrade.Datetime.year.ToString("0000") + "." + allTrade.Datetime.month.ToString("00") + "." + allTrade.Datetime.day.ToString("00");
                classAllTrade.Date = dateAllTrade;


                string timeAllTrade = allTrade.Datetime.hour.ToString("00") + ":" + allTrade.Datetime.min.ToString("00") + ":" +
                             allTrade.Datetime.sec.ToString("00") + "." + allTrade.Datetime.ms.ToString("000");
                classAllTrade.Time = timeAllTrade;
                classAllTrade.Price = (decimal)allTrade.Price;
                classAllTrade.Volume = (int)allTrade.Qty;
                classAllTrade.TradeNum = allTrade.TradeNum;


                int flags = (int)allTrade.Flags;
                int sell = 1025;
                int buy = 1026;

                if (sell == flags)
                {
                    classAllTrade.Flag = "Продажа";
                }
                if (buy == flags)
                {
                    classAllTrade.Flag = "Купля";
                }

                classAllTrade.OpenInterest = (int)allTrade.OpenInterest;


                // дельта времени прихода в сек
                classAllTrade.DeltaTime = (DateTime.Now - (DateTime)(allTrade.Datetime)).TotalSeconds;





                //MainWindow.Dispatcher.Invoke(() =>
                //{
                //    DGEventAllTrade.Items.Refresh();

                //    DGServerAllTrade.Items.Refresh();

                //});
            }
        }


        #endregion

        /************************************************************************************************************/
        public MainWindowViewModel()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var file_name = dialog.FileName;

            #region Команды

            CloseApplicationCommand2 = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            //ChangeTabIndexCommand = new LamdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            //CreateGroupCommand = new LamdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);

            //DeleteGroupCommand = new LamdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            //AllTradeCommand = new LamdaCommand(OnAllTradeCommandExecuted, CanAllTradeCommandExecute);

            #endregion

            /********************************************************************************************************************/
          
        }
    }
}
