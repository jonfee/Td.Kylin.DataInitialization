using System;
using System.Windows.Forms;

namespace Td.Kylin.DataInit.Core
{
    /// <summary>
    /// 进度更新
    /// </summary>
    public sealed  class ProgressUpdater
    {
        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value"></param>
        internal delegate void ProgressDelegate(int value);

        private volatile static ProgressUpdater _instance;

        private readonly static object mylock = new object();

        private Form _form;

        private ProgressDelegate _delegate;

        public static ProgressUpdater Instance
        {
            get { return _instance; }
        }
        internal static void InitInstance(Form form, ProgressDelegate progressDelegate)
        {
            if (_instance == null)
            {
                lock (mylock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProgressUpdater(form, progressDelegate);
                    }
                }
            }
        }

        private ProgressUpdater(Form form, ProgressDelegate progressDelegate)
        {
            this._form = form;
            _delegate = progressDelegate;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value"></param>
        public void Update(int value)
        {
            if (null != _form && null != _delegate)
            {
                _form.Invoke((EventHandler)delegate
                {
                    _delegate(value);
                });
            }
        }
    }
}
