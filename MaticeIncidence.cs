using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pokus
{
    internal class MaticeIncidence:I_Publisher
    {
        private List<I_Observer> observers = new List<I_Observer>();
        private List<List<int>> my_matrix;
        
        public MaticeIncidence()
        {
            my_matrix = new List<List<int>> { new List<int> { 1 } };
            this.NotifySubscribers();
        }

        public void AddModule()
        { 
            foreach (var row in my_matrix)
            {
                row.Add(0);
            }
            List<int> new_row = new List<int>();
            for (int i = 0; i <= my_matrix.Count; i++)
            {
                if (i != my_matrix.Count )
                {
                    new_row.Add(0);
                }
                else
                {
                    new_row.Add(1);
                }
                
            }
            my_matrix.Add(new_row);
            this.NotifySubscribers();
        }
        public void RemoveModule()
        {
            if (my_matrix.Count > 0)
            {
                foreach (var row in my_matrix)
                {
                    if (row.Count > 0)
                    {
                        row.RemoveAt(row.Count - 1);
                    }
                }
                my_matrix.RemoveAt(my_matrix.Count - 1);
            }
            this.NotifySubscribers();
        }

        
        public int GetModuleCount()
        {
            return my_matrix.Count();
        }
        public bool ModuleRelationship(int controlingModule, int controledModule)
        {
           if (my_matrix[controlingModule - 1][controledModule - 1] == 1)
           {
                    return true;
           }
           else
           {
                    return false;
           }
        }
        public void AddControl(int controlingModule, int controledModule)
        {
            my_matrix[controlingModule - 1][controledModule - 1] = 1;
            this.NotifySubscribers();
        }
        public void RemoveControl(int controlingModule, int controledModule)
        {
            my_matrix[controlingModule - 1][controledModule - 1] = 0;
            this.NotifySubscribers();
        }
        public void AddSubscriber(I_Observer observer)
        {
            observers.Add(observer);
            observer.Observe(this.DeepCopyMatrix());

        }

        public void RemoveSubscriber(I_Observer observer)
        {
            observers.Remove(observer);
            
        }
        private List<List<int>> DeepCopyMatrix()
        {
            List<List<int>> copy = new List<List<int>>();
            foreach (var row in this.my_matrix)
            {
                List<int> newRow = new List<int>(row);
                copy.Add(newRow);
            }
            return copy;
        }

        public void NotifySubscribers()
        {

            
            
            foreach (I_Observer observer in observers)
            {
                observer.Observe(this.DeepCopyMatrix());
            }
        }
    }
}
