using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SideChicksRestApi.Data;
using SideChicksRestApi.Helpers;
using Xunit;


namespace SideChicksRestApiTests;

public class EmailHelperTests()
{
    private readonly EmailHelper _helper;
    private readonly ApplicationDbContext _context;

    public ReviewControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase").Options;
        _context = new ApplicationDbContext(options);
        _controller = new ReviewController(_context);
    }
    
    [Fact]
    public void Test_Email_is_False()
    {
        var test = EmailHelper.userMailCheck("blah");

        test.Should.Contain("Not Eligable");
    }
