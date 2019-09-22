//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.ReceiptListDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class ReceiptListDataSource { }
#else

    public class ReceiptListDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ReceiptListDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/ReceiptListDataSource/ReceiptListDataSource.xaml", UriKind.RelativeOrAbsolute);
                System.Windows.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _EmailTitle = string.Empty;

        public string EmailTitle
        {
            get
            {
                return this._EmailTitle;
            }

            set
            {
                if (this._EmailTitle != value)
                {
                    this._EmailTitle = value;
                    this.OnPropertyChanged("EmailTitle");
                }
            }
        }

        private string _EmailBccAccountName = string.Empty;

        public string EmailBccAccountName
        {
            get
            {
                return this._EmailBccAccountName;
            }

            set
            {
                if (this._EmailBccAccountName != value)
                {
                    this._EmailBccAccountName = value;
                    this.OnPropertyChanged("EmailBccAccountName");
                }
            }
        }

        private string _EmailAccountName = string.Empty;

        public string EmailAccountName
        {
            get
            {
                return this._EmailAccountName;
            }

            set
            {
                if (this._EmailAccountName != value)
                {
                    this._EmailAccountName = value;
                    this.OnPropertyChanged("EmailAccountName");
                }
            }
        }

        private string _EmailSendBccAccountState = string.Empty;

        public string EmailSendBccAccountState
        {
            get
            {
                return this._EmailSendBccAccountState;
            }

            set
            {
                if (this._EmailSendBccAccountState != value)
                {
                    this._EmailSendBccAccountState = value;
                    this.OnPropertyChanged("EmailSendBccAccountState");
                }
            }
        }

        private string _EmailSendBccAccountSendTime = string.Empty;

        public string EmailSendBccAccountSendTime
        {
            get
            {
                return this._EmailSendBccAccountSendTime;
            }

            set
            {
                if (this._EmailSendBccAccountSendTime != value)
                {
                    this._EmailSendBccAccountSendTime = value;
                    this.OnPropertyChanged("EmailSendBccAccountSendTime");
                }
            }
        }
    }

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
    }
#endif
}
