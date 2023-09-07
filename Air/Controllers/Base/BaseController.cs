using Air.Data.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace Air.Controllers.Base;

public class BaseController : ControllerBase
{

    protected BaseController()
    {
    }

    protected IActionResult PageList<T>(List<T> list, int count, int maxPages)
    {
        return Ok(new PageListModel<T>()
        {
            List = list,
            Count = count,
            MaxPages = maxPages,
        });
    }
    
    protected IActionResult Created()
    {
        return StatusCode(StatusCodes.Status201Created);
    }

    protected IActionResult Created(int id)
    {
        return StatusCode(StatusCodes.Status201Created, new CreatedModel()
        {
            Id = id,
        });
    }
    
}