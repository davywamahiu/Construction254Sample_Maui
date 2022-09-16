using Construction_Ke.Views.AccountView;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction_Ke.ViewModel.AccountsVM
{
    public class AccountsViewModel
    {
        public AccountsViewModel()
        {
            LoadCompany = new(GetCompanyAcc);
            LoadSettingsAccounts = new(GetLoadSettingsAccounts);
            LoadFiscalYear = new(GetLoadFiscalYear);
            LoadAccountingDimension = new(GetLoadAccountingDimension);
            LoadFinanceBook = new(GetLoadFinanceBook);
            AccountingPeriod = new(GetAccountingPeriod);
            LoadPaymentTerm = new(GetLoadPaymentTerm);
            LoadAccReports = new(GetLoadAccReports);
            LoadWBPay = new(GetLoadWBPay);
            
        }
        public Command LoadWBPay { get; }
        public Command LoadAddNewWBPayment { get; }
        private async void GetLoadWBPay(object obj)
        {
            try
            {
                //await Shell.Current.DisplayAlert("Good", "Great day!!!" , "Continue");
                await Shell.Current.GoToAsync(nameof(WBAccount));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadAccReports(object obj)
        {
            try
            {
                //await Shell.Current.DisplayAlert("Good", "Great day!!!" , "Continue");
                await Shell.Current.GoToAsync(nameof(AccReports));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }

        private async void GetLoadPaymentTerm(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(PaymentTerm));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetAccountingPeriod(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AccPeriod));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadFinanceBook(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FinanceBooks));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadAccountingDimension(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AccDimension));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadFiscalYear(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(FiscalYears));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetLoadSettingsAccounts(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AccountSettingz));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        private async void GetCompanyAcc(object obj)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AccountCompany));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Continue");
            }
        }
        public Command LoadCompany { get; }
        public Command LoadSettingsAccounts { get; }
        public Command LoadFiscalYear { get; }
        public Command LoadAccountingDimension { get; }
        public Command LoadFinanceBook { get; }
        public Command AccountingPeriod { get; }
        public Command LoadPaymentTerm { get; }


        //Reports
        public Command LoadAccReports { get; }
    }
}
