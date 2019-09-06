using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apps.OSS.API.Application.Commands;
using Apps.OSS.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apps.OSS.API.Controllers
{
    [Route("OSS/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator mediator;
        private static volatile int conTick = 1;


        #region ctor
        public FilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FileIdentityQueryDTO), 200)]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            //var aaa = Request.Headers;

            var i = System.Threading.Interlocked.Increment(ref conTick);
            //var dto = await mediator.Send(new FileIdentityQuery(id), cancellationToken);
            //return Ok(dto);
            return Ok(i);
        }

        [HttpPost("Stream")]
        [ProducesResponseType(typeof(FileIdentityQueryDTO), 200)]
        public async Task<IActionResult> UploadStreamFile(CancellationToken cancellationToken)
        {
            var command = new FileCreateCommand();
            command.Name = "Leon";
            //var id = await mediator.Send(command);
            //return CreatedAtAction(nameof(Get), new { id = "23234" }, command);

            return await Get("123", cancellationToken);
        }











    }
}