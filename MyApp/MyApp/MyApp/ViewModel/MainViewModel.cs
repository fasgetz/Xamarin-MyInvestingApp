using InvestingCalcLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModel
{
    class MainViewModel : VMBase
    {

        #region Свойства

        // Логика
        private InvestingLogic _logic;
        public InvestingLogic logic
        {
            get => _logic;
            set
            {
                _logic = value;
                OnPropertyChanged("logic");
            }
        }

        string url; // url 

        public string StartDate
        {
            get => logic.GetDateInvesting.ToString("D");
        }

        public double StartSum
        {
            get => logic.GetStartCapital;
        }

        public double StartPriceShare
        {
            get => logic.GetStartShareBuying;
        }

        public double GetCountShares
        {
            get => logic.GetCountShare;
        }

        public double GetCurrentSharePrice
        {
            get => logic.GetCurrentPriceShare;
            set
            {
                OnPropertyChanged("GetCurrentSharePrice");
            }
        }


        public string CurrentDate
        {
            get => logic.GetCurrentDate;
            set
            {

                OnPropertyChanged("CurrentDate");
            }
        }

        public double GetProcent
        {
            get => logic.GetDifferenceProcent;
            set
            {
                OnPropertyChanged("GetProcent");
            }
        }

        public double EarningSum
        {
            get => logic.GetEarningSum;
            set
            {
                OnPropertyChanged("EarningSum");
            }
        }

        public double GetCurrentCapital
        {
            get => logic.GetCurrentCapital;
            set
            {
                OnPropertyChanged("GetCurrentCapital");
            }
        }

        #endregion

        // Метод для загрузки данных
        public async Task<bool> LoadData()
        {
            try
            {
                return await Task.Run(() =>
                {
                    logic.LoadHtml(url);

                    
                    return true;
                });
            }
            catch (FormatException)
            {
                return true;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка", "Нет подключения к интернету", "ОК");

                return false;
            }


        }

        public async void Init()
        {
            bool res = await LoadData();

            // Если res == true, то данные загружены
            if (res == true)
            {
                logic.InitializationFromXamarin();

                GetCurrentSharePrice = logic.GetCurrentPriceShare;
                CurrentDate = logic.GetCurrentDate;
                EarningSum = logic.GetEarningSum;
                GetProcent = logic.GetDifferenceProcent;
                GetCurrentCapital = logic.GetCurrentCapital;

                //await App.Current.MainPage.DisplayAlert($"true {GetCurrentSharePrice}", "Нет подключения к интернету", "ОК");
            }
        }

        public MainViewModel()
        {
            logic = new InvestingLogic(new DateTime(2019, 10, 14), 6290.69, 2000000);

            url = "https://investfunds.ru/funds/268/";

            Init(); // Инициализируем данные

        }
    }
}
