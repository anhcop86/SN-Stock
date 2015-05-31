using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VfsInformationFeedService
{
    public class MainThread : IMainThread
    {
        private Thread thread;

        public MainThread()
        {
            thread = new Thread(new ThreadStart(Execute));
        }

        public void Start()
        {
            try
            {
                this.thread.Start();
            }
            catch(Exception e)
            {
                //log exception
            }
        }

        public void Stop()
        {
            try
            {
                if (this.thread.IsAlive)
                {
                    this.thread.Abort();
                }
            }
            catch (Exception e)
            {
                //log exception
            }
        }

        public void Pause()
        {
            try
            {
                if (this.thread.IsAlive)
                {
                    this.thread.Suspend();
                }
            }
            catch (Exception e)
            {
                //log exception
            }
        }

        public void Resume()
        {
            try
            {
                if (this.thread.IsAlive)
                {
                    this.thread.Resume();
                }
            }
            catch (Exception e)
            {
                //log exception
            }
        }

        public virtual void Execute()
        {
            
        }
    }
}
