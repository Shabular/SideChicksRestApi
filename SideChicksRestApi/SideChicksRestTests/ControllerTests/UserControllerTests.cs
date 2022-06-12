using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SideChicksRestApi.Controllers;
using SideChicksRestApi.Data;
using SideChicksRestApi.Models;
using Moq;
using Xunit;
using Assert = Xunit.Assert;



namespace SideChicksRestTests;

public class UserControllerTests
{
    private readonly UserController _sut ;
    private readonly ApplicationDbContext _context;
    private bool seededUsers;

    
    [SetUp]
    public void Setup()
    {
        var _context = new Mock<ApplicationDbContext>();
        var _sut = new UserController( _context.Object);

        seededUsers = UserController.SeedUsers(_context.Object);
    }

    [Test]
    public void Test_Seed_Users_Filled_DB()
    {
        
        Assert.True(seededUsers);
    }
    
    
}