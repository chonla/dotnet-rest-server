using System.IO;
using Newtonsoft.Json;
using restapi.Models;
using System.Collections.Generic;

namespace restapi.Services {
    public class CustomersService {
        private List<Customer> data;

        public CustomersService () {
            Customer[] d = JsonConvert.DeserializeObject<Customer[]> (File.ReadAllText ("data.json"));
            this.data = new List<Customer>();
            for (int i = 0; i < d.Length; i++) {
                this.data.Add(d[i]);
            }
        }

        public Customer[] All () {
            return this.data.ToArray();
        }

        public Customer Get (int id) {
            int index = this.IndexOf (id);
            if (index >= 0) {
                return this.data[index];
            }
            return null;
        }

        private int IndexOf (int id) {
            for (int i = 0; i < this.data.Count; i++) {
                if (this.data[i].ID == id) {
                    return i;
                }
            }
            return -1;
        }

        public bool Exists (int id) {
            return this.IndexOf (id) >= 0;
        }

        public void Update (int id, Customer cust) {
            int index = this.IndexOf (id);
            if (index >= 0) {
                this.data[index] = cust;

                this.flush ();
            }
        }

        public void Add (Customer cust) {
            this.data.Add(cust);
            this.flush();
        }

        private void flush () {
            var new_data = JsonConvert.SerializeObject (this.data.ToArray(), Formatting.Indented);
            File.WriteAllText ("data.json", new_data);
        }

        public void Remove(int id) {
            int index = this.IndexOf (id);
            if (index >= 0) {
                this.data.RemoveAt(index);

                this.flush ();
            }
        }

        public int NextId () {
            int max = 0;
            for (int i = 0; i < this.data.Count; i++) {
                if (this.data[i].ID > max) {
                    max = this.data[i].ID;
                }
            }
            return max + 1;
        }
    }
}