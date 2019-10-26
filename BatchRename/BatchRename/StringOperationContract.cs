﻿using System.ComponentModel;
using PropertyChanged;
namespace BatchRename
{
    public class StringArgs
    {
    }

    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public abstract class StringOperation : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// thực hiện nghiệp vụ đổi tên ở đây
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public abstract string Operate(string origin);

        public abstract string Name { get; }
        public abstract string Description { get; }
        /// <summary>
        /// Clone ra 1 operation từ mẫu prototype, hiện dialog để người dùng customize
        /// </summary>
        /// <returns></returns>
        public abstract StringOperation Clone();
        /// <summary>
        /// Người dùng chọn edit, hiện dialog để người dùng config lại operation
        /// </summary>
        public abstract void Config();
    }

    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            //tạo mới 1 replaceOperation, set from và to là ""
            var newReplaceOperation = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "",
                    To = ""
                }
            };
            //hiện dialog để người dùng customize
            var screen = new ReplaceConfigDialog(newReplaceOperation.Args);
            if (screen.ShowDialog() == true)
            {
                
            }
            //lấy args sau khi người dùng config, nếu nó vẫn là from"" to "" thì không return newReplaceOperation
            //đó là trường hợp người dùng cancel
            var ArgsAfterConfiguration = newReplaceOperation.Args as ReplaceArgs;
            if (ArgsAfterConfiguration.From == "" && ArgsAfterConfiguration.To == "")
            {
                return null;
            }
            else
            {
                //trả về replaceOperation mà người dùng đã custom
                return newReplaceOperation;
            }
        }

        public override void Config()
        {
            var screen = new ReplaceConfigDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override string Name => "Replace";
        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                
                return $"Replace from {args.From} to {args.To}";
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Description));

        }
    }
}
