using System;
using System.Windows.Forms;

namespace Td.Kylin.DataInit.Core
{
    /// <summary>
    /// 消息输出
    /// </summary>
    public sealed class MsgWriter
    {
        /// <summary>
        /// 消息输出委托
        /// </summary>
        /// <param name="message"></param>
        /// <param name="padTime"></param>
        internal delegate void WriterDelegate(string message, bool padTime = true);

        private volatile static MsgWriter _instance;

        private readonly static object mylock = new object();

        private Form _form;

        private WriterDelegate _delegate;

        public static MsgWriter Instance
        {
            get { return _instance; }
        }
        internal static void InitInstance(Form form, WriterDelegate writedelegate)
        {
            if (_instance == null)
            {
                lock (mylock)
                {
                    if (_instance == null)
                    {
                        _instance = new MsgWriter(form, writedelegate);
                    }
                }
            }
        }

        private MsgWriter(Form form, WriterDelegate writedelegate)
        {
            this._form = form;
            _delegate = writedelegate;
        }

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="form"></param>
        /// <param name="writerDelegate"></param>
        /// <param name="message"></param>
        /// <param name="padTime"></param>
        public void Write(string message, bool padTime = true)
        {
            if (null != _form && null != _delegate)
            {
                _form.Invoke((EventHandler)delegate
                {
                    _delegate(message, padTime);
                });
            }
        }
    }
}
