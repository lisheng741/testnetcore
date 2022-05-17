using EFCodeTest.Data;
using EFCodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCodeTest.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly EDbContext _db;

    public OrderController(EDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public Order? Get(int id)
    {
        Order? Order = _db.Order.Include(b => b.Lines).Where(t => t.Id == id).FirstOrDefault();
        return Order;
    }

    [HttpPost]
    public void Post(Order Order)
    {
        _db.Order.Add(Order);
        _db.SaveChanges();
    }

    [HttpGet]
    [Route("UnitOfWork")]
    public void UnitOfWorkTest()
    {
        _db.Order.Add(new Order() { Name = "1" });
        _db.Order.Add(new Order() { Name = "2" });
        _db.SaveChanges();

        _db.Order.Add(new Order() { Name = "fail" });
        _db.Order.Add(new Order() { Name = "1234567890123456789012345678901234567890" });
        _db.SaveChanges();
    }

    [HttpDelete]
    public bool Delete(int id)
    {
        Order? Order = _db.Order.Find(id) ?? null;
        if (Order == null) return false;
        _db.Order.Remove(Order);
        _db.SaveChanges();
        return true;
    }

    [HttpDelete]
    [Route("DeleteCascade")]
    public bool DeleteCascade(int id)
    {
        Order? Order = _db.Order.Include(t => t.Lines).Where(t => t.Id == id).FirstOrDefault() ?? null;
        if (Order == null) return false;
        _db.Order.Remove(Order);
        _db.SaveChanges();
        return true;
    }

    [HttpDelete]
    [Route("DeleteNull")]
    public bool DeleteNull(int id)
    {
        Order? Order = _db.Order.Include(t => t.Lines).Where(t => t.Id == id).FirstOrDefault() ?? null;
        if (Order == null) return false;
        Order.Lines = null;
        _db.Order.Remove(Order);
        _db.SaveChanges();
        return true;
    }
}
