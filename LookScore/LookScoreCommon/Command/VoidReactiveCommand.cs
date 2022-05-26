using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace LookScoreCommon.Command
{
    public class VoidReactiveCommand : ReactiveCommand<Unit, Unit>
    {
        public VoidReactiveCommand(Func<Unit, IObservable<Unit>> execute, IObservable<bool> canExecute, IScheduler outputScheduler) :
            base(execute, canExecute, outputScheduler, outputScheduler)
        { }

        public static VoidReactiveCommand Create(Action execute, IObservable<bool> canExecute = null, IScheduler outputScheduler = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return new VoidReactiveCommand(
                _ => Observable.Create<Unit>(
                    observer =>
                    {
                        execute();
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                        return Disposable.Empty;
                    }),
                canExecute ?? Observable.Return(true),
                outputScheduler ?? RxApp.MainThreadScheduler);
        }
    }

    public class VoidReactiveCommand<T> : ReactiveCommand<T, Unit>
    {
        public VoidReactiveCommand(Func<T, IObservable<Unit>> execute, IObservable<bool> canExecute, IScheduler outputScheduler) :
            base(execute, canExecute, outputScheduler, outputScheduler)
        { }

        public static VoidReactiveCommand<T> Create(Action<T> execute, IObservable<bool> canExecute = null, IScheduler outputScheduler = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            return new VoidReactiveCommand<T>(
                param => Observable.Create<Unit>(
                    observer =>
                    {
                        execute(param);
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                        return Disposable.Empty;
                    }),
                canExecute ?? Observable.Return(true),
                outputScheduler ?? RxApp.MainThreadScheduler);
        }
    }
}
