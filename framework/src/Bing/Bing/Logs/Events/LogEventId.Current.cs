using System;
using System.Threading;

namespace Bing.Logs.Events
{
    public partial class LogEventId
    {
        /// <summary>
        /// 当前日志事件标识
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly AsyncLocal<LogEventId> _currentLogEventId = new AsyncLocal<LogEventId>();

        /// <summary>
        /// 是否更新事件标识
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly AsyncLocal<NeedUpdateCurrentEventId> _bool = new AsyncLocal<NeedUpdateCurrentEventId>();

        // 静态构造函数
        static LogEventId() => _bool.Value = true;

        /// <summary>
        /// 当前日志事件标识
        /// </summary>
        public static LogEventId Current
        {
            get => _currentLogEventId.Value;
            internal set => _currentLogEventId.Value = value;
        }

        /// <summary>
        /// 是否需要更新当前值
        /// </summary>
        internal static bool NeedUpdateCurrentValue
        {
            get => _bool.Value.Value;
            set => _bool.Value = value;
        }

        /// <summary>
        /// 更新 root 日志事件标识
        /// </summary>
        /// <param name="root">root 日志事件标识</param>
        /// <param name="strategy">更新策略</param>
        public static void UpdateRootEventId(LogEventId root, UpdateStrategy strategy = UpdateStrategy.ThrowIfError)
        {
            var current = Current;
            switch (strategy)
            {
                case UpdateStrategy.Force:
                    {
                        Current = root ??
                                  throw new ArgumentNullException(nameof(root), "Root log event id cannot be null.");
                        NeedUpdateCurrentValue = false;
                        break;
                    }
                case UpdateStrategy.Safety:
                    {
                        if (root != null)
                        {
                            Current = root;
                            NeedUpdateCurrentValue = false;
                        }
                        break;
                    }
                case UpdateStrategy.ThrowIfError:
                    {
                        if (root == null)
                            throw new ArgumentNullException(nameof(root), "Root log event id cannot be null.");
                        if (current != null)
                            throw new InvalidOperationException(
                                "Current event id has been set, you cannot set another EventId right now");
                        Current = root;
                        NeedUpdateCurrentValue = false;
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null);
            }
        }

        /// <summary>
        /// 更新 root 日志事件标识策略
        /// </summary>
        public enum UpdateStrategy
        {
            /// <summary>
            /// 如果root当前日志事件标识已设置，则抛出异常
            /// </summary>
            ThrowIfError,
            /// <summary>
            /// 强制更新
            /// </summary>
            Force,
            /// <summary>
            /// 安全更新
            /// </summary>
            Safety,
        }

        /// <summary>
        /// 是否更新当前事件标识
        /// </summary>
        private readonly struct NeedUpdateCurrentEventId
        {
            public bool Value { get; }

            private NeedUpdateCurrentEventId(bool value) => Value = value;

            public static implicit operator bool(NeedUpdateCurrentEventId value) => value.Value;

            public static implicit operator NeedUpdateCurrentEventId(bool value) => new NeedUpdateCurrentEventId(value);
        }
    }
}
