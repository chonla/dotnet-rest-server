using RestServer.Models;

namespace RestServer.Services
{
  public interface ICustomersService
  {
      Customer[] All ();
      Customer Get (int id);
      bool Exists (int id);
      void Update (int id, Customer cust);
      void Add (Customer cust);
      void Remove(int id);
      int NextId ();

  }
}