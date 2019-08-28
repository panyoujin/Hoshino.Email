//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.MainDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class MainDataSource { }
#else

    public class MainDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MainDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/MainDataSource/MainDataSource.xaml", UriKind.RelativeOrAbsolute);
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

        private double _Num = 0;

        public double Num
        {
            get
            {
                return this._Num;
            }

            set
            {
                if (this._Num != value)
                {
                    this._Num = value;
                    this.OnPropertyChanged("Num");
                }
            }
        }

        private string _Email = string.Empty;

        public string Email
        {
            get
            {
                return this._Email;
            }

            set
            {
                if (this._Email != value)
                {
                    this._Email = value;
                    this.OnPropertyChanged("Email");
                }
            }
        }

        private string _Group = string.Empty;

        public string Group
        {
            get
            {
                return this._Group;
            }

            set
            {
                if (this._Group != value)
                {
                    this._Group = value;
                    this.OnPropertyChanged("Group");
                }
            }
        }

        private string _Status = string.Empty;

        public string Status
        {
            get
            {
                return this._Status;
            }

            set
            {
                if (this._Status != value)
                {
                    this._Status = value;
                    this.OnPropertyChanged("Status");
                }
            }
        }

        private string _CreateTime = string.Empty;

        public string CreateTime
        {
            get
            {
                return this._CreateTime;
            }

            set
            {
                if (this._CreateTime != value)
                {
                    this._CreateTime = value;
                    this.OnPropertyChanged("CreateTime");
                }
            }
        }
    }

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
    }
#endif
}
