using A011MainPavel001PortN07.Commands;
using A011MainPavel001PortN07.Models;
using A011MainPavel001PortN07.QuikBase;
using A011MainPavel001PortN07.ViewModels.Base;
using Microsoft.Win32;
using QuikSharp;
using QuikSharp.DataStructures;
using QuikSharp.DataStructures.Transaction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace A011MainPavel001PortN07.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        #region Quik

        private Quik _quik;
        string secCode;
        string classCode = "";
        string clientCode;
        private Tool tool;
        OrderBook toolOrderBook;

        ClassAllTrade eventAllTrade = new ClassAllTrade();
        ClassParam eventParam = new ClassParam();
        ClassQuote eventQuote = new ClassQuote();
        ClassMoneyLimitEx eventMoney = new ClassMoneyLimitEx();
        ClassDepoLimitEx eventDepo = new ClassDepoLimitEx();
        ClassOrder eventOrder = new ClassOrder();

        ClassAllTrade loadAllTrade = new ClassAllTrade();

        List<ClassAllTrade> collectionDGeventAllTrade = new List<ClassAllTrade>();
        List<ClassAllTrade> collectionDGLoadAllTrade = new List<ClassAllTrade>();
        List<ClassParam> collectionDGeventParam = new List<ClassParam>();
        List<ClassMoneyLimitEx> collecEventMoney = new List<ClassMoneyLimitEx>();
        List<ClassDepoLimitEx> collecEventDepoLimit = new List<ClassDepoLimitEx>();
        List<ClassOrder> collecEventOrder = new List<ClassOrder>();

        ObservableCollection<ClassAllTrade> collectionDG2 = null;
        ObservableCollection<ClassAllTrade> dbCollectionSber = null;

        private bool _isSubscribedToolOrderBook = false;
        private bool _isServerConnected = false;

        public QuikArtemDB db;     // инициализация контекста

        #endregion


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

        #region QuikAllConnectCommand

        public ICommand QuikAllConnectCommand { get; }

        private bool CanQuikAllConnectCommandExecute(object p) => true;

        private void OnQuikAllConnectCommandExecuted(object p)
        {
            AllQuikConnect();
        }



        #endregion

        /************************************************************************************************************/
        public MainWindowViewModel()
        {
            db = new QuikArtemDB();     // инициализация контекста


            SaveFileDialog dialog = new SaveFileDialog();
            var file_name = dialog.FileName;

            #region Команды

            CloseApplicationCommand2 = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            QuikAllConnectCommand = new LamdaCommand(OnQuikAllConnectCommandExecuted, CanQuikAllConnectCommandExecute);
            //ChangeTabIndexCommand = new LamdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            //CreateGroupCommand = new LamdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);

            //DeleteGroupCommand = new LamdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);



            #endregion

            /********************************************************************************************************************/

        }

        #region События

        private void Events_OnMoneyLimit(MoneyLimitEx mLimit)
        {
            eventMoney.LimitKind = mLimit.LimitKind;
            eventMoney.CurrentBal = mLimit.CurrentBal;
            eventMoney.CurrentLimit = mLimit.CurrentLimit;
            eventMoney.Locked = mLimit.Locked;
            eventMoney.OpenBal = mLimit.OpenBal;
            eventMoney.OpenLimit = mLimit.OpenLimit;
            eventMoney.OrdersCollateral = mLimit.OrdersCollateral;
            eventMoney.PositionsCollateral = mLimit.PositionsCollateral;
            eventMoney.Tag = mLimit.Tag;
            eventMoney.WaPositionPrice = mLimit.WaPositionPrice;

            db.ClassMoneyLimitExs.Add(eventMoney);
            db.SaveChanges();


            Log("Events_OnMoneyLimit(): Изменилась позиция по деньгам: CurrentBal=" + mLimit.CurrentBal + " OpenBal=" + mLimit.OpenBal + " Locked=" + mLimit.Locked);
        }

        private void Events_OnDepoLimit(DepoLimitEx dLimit)
        {
            if (dLimit.SecCode == secCode && dLimit.LimitKindInt == 0) // T0
            {
                eventDepo.AweragePositionPrice = dLimit.AweragePositionPrice;
                eventDepo.CurrentBalance = dLimit.CurrentBalance;
                eventDepo.CurrentLimit = dLimit.CurrentLimit;
                eventDepo.OpenBalance = dLimit.OpenBalance;
                eventDepo.OpenLimit = dLimit.OpenLimit;
                eventDepo.SecCode = dLimit.SecCode;

                Log("Events_OnDepoLimit(): Изменилась позиция по бумагам: " + dLimit.SecCode + " CurrentBalance=" + dLimit.CurrentBalance + " AweragePositionPrice=" + dLimit.AweragePositionPrice);
            }

            //collecEventDepoLimit.Add(eventDepo);

            db.ClassDepoLimitExs.Add(eventDepo);
            db.SaveChanges();

 
        }

        private void Events_OnParam(Param par)
        {
            if (par.SecCode == secCode)
            {
                eventParam.Name = par.SecCode;


                eventParam.TimeSave = DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss.ffff");

                // Дата торгов

                eventParam.Date = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "TRADE_DATE_CODE").Result.ParamImage;




                // CHANGETIME       // Время последнего изменения

                eventParam.Time = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "CHANGETIME").Result.ParamImage;


                //string str = allTrade.Datetime.hour.ToString("00") + ":" + allTrade.Datetime.min.ToString("00") + ":" +
                //                 allTrade.Datetime.sec.ToString("00") + "." + allTrade.Datetime.ms.ToString("000");
                //eventAllTrade.Time = str;


                //string timeParams=_quik.Trading.GetParamEx2


                //NUMBIDS       NUMERIC  Количество заявок на покупку 
                string quaOrderBuy = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "NUMBIDS").Result.ParamImage;

                string numericStringBuy = String.Empty;
                numericStringBuy = "";

                foreach (var c in quaOrderBuy)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericStringBuy = String.Concat(numericStringBuy, c);
                    }

                }
                if (int.TryParse(numericStringBuy, out int sBuy))
                {
                    eventParam.NumberOfBuy = sBuy;
                }

                //NUMOFFERS     NUMERIC  Количество заявок на продажу 
                string quaOrderSell = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "NUMOFFERS").Result.ParamImage;
                string numericStringSell = String.Empty;
                numericStringSell = "";

                foreach (var c in quaOrderSell)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericStringSell = String.Concat(numericStringSell, c);
                    }

                }
                if (int.TryParse(numericStringSell, out int sSell))
                {
                    eventParam.NumberOfSell = sSell;
                }

                //BIDDEPTHT     NUMERIC  Суммарный спрос // Спрос по лучшей цене
                string tDemand = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "BIDDEPTHT").Result.ParamImage;
                string numericStringtDemand = String.Empty;
                numericStringtDemand = "";

                foreach (var c in tDemand)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericStringtDemand = String.Concat(numericStringtDemand, c);
                    }

                }
                if (int.TryParse(numericStringtDemand, out int tDd))
                {
                    eventParam.TotalDemand = tDd;
                }


                //OFFERDEPTHT   NUMERIC  Суммарное предложение  // Предложение по лучшей цене
                string tOffer = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "OFFERDEPTHT").Result.ParamImage;
                string numericStringtOffer = String.Empty;
                numericStringtOffer = "";

                foreach (var c in tOffer)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericStringtOffer = String.Concat(numericStringtOffer, c);
                    }

                }
                if (int.TryParse(numericStringtOffer, out int tOr))
                {
                    eventParam.TotalOffer = tOr;
                }

                ////NUMCONTRACTS  NUMERIC  Количество открытых позиций
                string OpenPosish = _quik.Trading.GetParamEx2(par.ClassCode, par.SecCode, "NUMCONTRACTS").Result.ParamImage;
                string numericStringOpenPosish = String.Empty;
                numericStringtOffer = "";

                foreach (var c in OpenPosish)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericStringOpenPosish = String.Concat(numericStringOpenPosish, c);
                    }

                }
                if (int.TryParse(numericStringOpenPosish, out int OP))
                {
                    eventParam.OpenPositions = OP;
                }

                db.ClassParams.Add(eventParam);
                db.SaveChanges();

        

            }

        }

        private void Events_OnQuote(OrderBook orderbook)
        {

            if (orderbook.sec_code == secCode)
            {
                var bestBid = orderbook.bid[orderbook.bid.Length - 1];
                var bestAsc = orderbook.offer[0];

                eventQuote.Name = String.Format("{0}---{1}***{2}---{3}", bestBid.price, bestBid.quantity, bestAsc.price, bestAsc.quantity);
                db.ClassQuotes.Add(eventQuote);
                db.SaveChanges();

                //Log(output);
                //EnterLong((decimal)bestBid.price + 1 * tool.Step);
                //EnterShort((decimal)bestAsc.price - 1 * tool.Step);
            }

        }

        private void Candles_NewCandle(Candle candle)
        {
            Log(candle.ToString());
        }

        private void Events_OnOrder(Order order)
        {

            if (order.TransID > 0)
            {
                eventOrder.Account = order.Account;
                eventOrder.Balance = order.Balance;
                eventOrder.ClassCode = order.ClassCode;
                eventOrder.Datetime = order.Datetime;
                eventOrder.ExtOrderStatus = order.ExtOrderStatus;
                eventOrder.Flags = order.Flags;
                eventOrder.Linkedorder = order.Linkedorder;
                eventOrder.Operation = order.Operation;
                eventOrder.OrderNum = order.OrderNum;
                eventOrder.Price = order.Price;
                eventOrder.Quantity = order.Quantity;
                eventOrder.SecCode = order.SecCode;
                eventOrder.State = order.State;
                eventOrder.TransID = order.TransID;
                eventOrder.Value = order.Value;

            }

            db.ClassOrders.Add(eventOrder);
            db.SaveChanges();



            Log("OrderNum = " + order.OrderNum + " TransID = " + order.TransID);
        }

        private void Events_OnAllTrade(AllTrade allTrade)
        {
            // Log(allTrade.TradeNum+ " "+ allTrade.SecCode+ " "+ allTrade.Price+" "+allTrade.Qty+" "+allTrade.Flags);
            if (allTrade.SecCode == secCode)
            {
                eventAllTrade.Name = allTrade.SecCode;
                eventAllTrade.TimeSave = DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss.ffff");

                string dateAllTrade = allTrade.Datetime.year.ToString("0000") + "." + allTrade.Datetime.month.ToString("00") + "." + allTrade.Datetime.day.ToString("00");
                eventAllTrade.Date = dateAllTrade;

                string timeAllTrade = allTrade.Datetime.hour.ToString("00") + ":" + allTrade.Datetime.min.ToString("00") + ":" +
                             allTrade.Datetime.sec.ToString("00") + "." + allTrade.Datetime.ms.ToString("000");
                eventAllTrade.Time = timeAllTrade;
                eventAllTrade.Price = (decimal)allTrade.Price;
                eventAllTrade.Volume = (int)allTrade.Qty;
                eventAllTrade.TradeNum = allTrade.TradeNum;

                int flags = (int)allTrade.Flags;

                int buyJ = 2;
                int sellJ = 1;

                int sell = 1025;
                int buy = 1026;

                if (sell == flags || sellJ == flags)
                {
                    eventAllTrade.Flag = "Продажа";
                }
                if (buy == flags || buyJ == flags)
                {
                    eventAllTrade.Flag = "Купля";
                }

                eventAllTrade.OpenInterest = (int)allTrade.OpenInterest;


                // дельта времени прихода в сек
                eventAllTrade.DeltaTime = (DateTime.Now - (DateTime)(allTrade.Datetime)).TotalSeconds;

                //DateTime date = new DateTime(1970, 1, 1).AddMilliseconds(orderBook.LuaTimeStamp) + TimeSpan.FromHours(3);

                //ClassAllTrade classAllTrade = new ClassAllTrade();        // лишнее
                //classAllTrade = eventAllTrade;

                loadAllTrade.Name = allTrade.SecCode;

                loadAllTrade.TradeNum = allTrade.TradeNum;

                //collectionDGeventAllTradeServer.Add(eventAllTrade);

                db.ClassAllTrades.Add(eventAllTrade);

                if (eventAllTrade.TradeNum != 0)
                {

                    db.SaveChanges();
                }

       
            }
        }

        #endregion

        #region Методы

        #region Подключение к Quik
        void Run()
        {
            try
            {
                Log("Определяем код класса инструмента " + secCode + ", по списку классов" + "...");
                try
                {
                    classCode = _quik.Class.GetSecurityClass("SPBFUT,TQBR,TQBS,TQNL,TQLV,TQNE,TQOB,QJSIM", secCode).Result;
                }
                catch
                {
                    Log("Ошибка определения класса инструмента. Убедитесь, что тикер указан правильно");
                }
                if (classCode != null && classCode != "")
                {
                    Log("Определяем код клиента...");
                    clientCode = _quik.Class.GetClientCode().Result;

                    Log("Создаем экземпляр инструмента " + secCode + "|" + classCode + "...");

                    tool = new Tool(_quik, secCode, classCode, 0);

                    if (tool != null && tool.Name != null && tool.Name != "")
                    {
                        Log("Инструмент " + tool.Name + " создан.");

                        Log("Подписываемся на стакан...");
                        _quik.OrderBook.Subscribe(tool.ClassCode, tool.SecurityCode).Wait();
                        _isSubscribedToolOrderBook = _quik.OrderBook.IsSubscribed(tool.ClassCode, tool.SecurityCode).Result;
                        if (_isSubscribedToolOrderBook)
                        {
                            toolOrderBook = new OrderBook();
                            Log("Подписка на стакан прошла успешно.");
                            Log("Подписываемся на коллбэк 'OnQuote'...");
                            _quik.Events.OnQuote += Events_OnQuote;
                        }
                        else
                        {
                            Log("Подписка на стакан не удалась.");
                        }
                        /**/
                        _quik.Events.OnOrder += Events_OnOrder;
                        _quik.Candles.Subscribe(classCode, secCode, CandleInterval.M1);
                        //_quik.Candles.NewCandle += Candles_NewCandle;
                        _quik.Events.OnAllTrade += Events_OnAllTrade;

                        _quik.Events.OnParam += Events_OnParam;

                        _quik.Events.OnDepoLimit += Events_OnDepoLimit;
                        _quik.Events.OnMoneyLimit += Events_OnMoneyLimit;



                        Log("Подключение eventoff пройдено.");
                  
                    }
                }
            }
            catch
            {
                Log("Ошибка получения данных по инструменту.");

            }
        }

        private void Sleep()
        {
            Thread.Sleep(5000);
        }

        public async void AllQuikConnect()  //Подключение к Quik
        {
           
            try
            {
                Log("Подключаемся к терминалу Quik...");

                await Task.Run(() => TestTime());


            }
            catch
            {
                Log("Ошибка инициализации объекта Quik...");
            }

            if (_quik != null)
            {
                Log("Экземпляр Quik создан.");
                try
                {

                    //F1:

                    Log("Получаем статус соединения с сервером....");
                    _isServerConnected = _quik.Service.IsConnected().Result;


                    if (_isServerConnected)
                    {

                        Log("Соединение с сервером установлено.");

                     


                    }
                    else
                    {
                        Log("Соединение с сервером НЕ установлено.");

                        // await Task.Run(() => Sleep());
                        Log("Пошёл на новый круг");

                        //goto F1;
                    }
                }
                catch
                {
                    Log("Неудачная попытка получить статус соединения с сервером.");
                }
            }
            Run();
        }

        public void TestTime()     // Обвязка для подключение к Quik
        {
            int timeAllMinutes;

            int nowAllMinutes;

            do
            {
                string timeNow = DateTime.Now.ToString("HH:mm:ss:fff");

                //string timeConst = "09:50:00";

                int timeHour = 0;

                int timeMinutes = 00;

                timeAllMinutes = timeHour * 60 + timeMinutes;

                string nowHour = DateTime.Now.ToString("HH");
                int intNowHour = Convert.ToInt32(nowHour);

                string nowMinutes = DateTime.Now.ToString("mm");
                int intnowMinutes = Convert.ToInt32(nowMinutes);

                nowAllMinutes = intNowHour * 60 + intnowMinutes;

                if (timeAllMinutes > nowAllMinutes)
                {
                    Log("Рано ещё!!! ");
                    Thread.Sleep(180000);
                }

                else
                {
                    Log("Настало время!!! ");
                    _quik = new Quik(Quik.DefaultPort, new InMemoryStorage());



                }



                Log("Привет мир");

                Log(timeNow);

                //Log(timeConst);



            } while (timeAllMinutes > nowAllMinutes);

            Log("Победа!!!");
        }

        #endregion


        public void KillOrder()
        {
            var orders = _quik.Orders.GetOrders(classCode, secCode).Result;
            foreach (var order in orders)
            {
                if (order.State == State.Active)
                {
                    _quik.Orders.KillOrder(order);
                }
            }
            Log("Сняты все заявки!");
        }


        public void KillStopOrders()
        {
            var stopOrders = _quik.StopOrders.GetStopOrders(classCode, secCode).Result;
            foreach (var stopOrder in stopOrders)
            {
                if (stopOrder.State == State.Active)
                {
                    _quik.StopOrders.KillStopOrder(stopOrder);
                }
            }
            Log("Сняты все СТОПзаявки!");
        }

        public void KillAllPosetions()
        {
            var allPosetions = _quik.Trading.GetDepoLimits(secCode).Result[1].CurrentBalance / tool.Lot;

            if (allPosetions != 0)
            {
                if (allPosetions > 0)
                {
                    _quik.Orders.SendMarketOrder(classCode, secCode, tool.AccountID, Operation.Sell, (int)allPosetions);
                }
                if (allPosetions < 0)
                {
                    _quik.Orders.SendMarketOrder(classCode, secCode, tool.AccountID, Operation.Buy, (int)-allPosetions);
                }
            }

            Log(allPosetions.ToString());
        }

        private string stringLog;

        ObservableCollection<String> logers;

        private void Log(string str)
        {
            try
            {
                logers = new ObservableCollection<String>();


                logers.Add("fgd");

                   // logers.Add(DateTime.Now.ToString("yyyy.MM.dd  HH:mm:ss.ffff") + " " + str + Environment.NewLine);



                    //TBLog.ScrollToLine(TBLog.LineCount - 1);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
        #endregion
    }
}
