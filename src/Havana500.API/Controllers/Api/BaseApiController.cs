using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Havana500.Business.Base;
using Havana500.Domain.Base;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace Havana500.Controllers.Api
{//TODO: map the result to the viewModels
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class BaseApiController<TApplicationService, 
        TEntity, 
        TKey, 
        TBaseViewModel,
        TCreateViewModel, 
        TEditViewModel,
        TIndexViewModel> : Controller
        where TApplicationService : IBaseApplicationService<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        public string CurrentUserId { get; set; }//TODO: move this to a context

        /// <summary>
        ///     The appSerices for the current controller.
        /// </summary>
        protected virtual TApplicationService ApplicationService { get; set; }

        /// <summary>
        ///     An <see cref="AutoMapper"/> instance.
        /// </summary>
        protected IMapper Mapper { get; set; }

        public BaseApiController(TApplicationService appService, IMapper mapper)
        {
            //CurrentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationService = appService;
            Mapper = mapper;           
        }

        /// <summary>
        ///     Gets a set of entities using pagination.
        /// </summary>
        /// <param name="pageSize">The amount of object per each page.</param>
        /// <param name="pageNumber">The current page to display.</param>
        /// <returns>The entity with ID=<paramref name="id"/>, null if not found</returns>
        /// <response code="200">When the entity is found by its id</response>
        [HttpGet()]
        public virtual IActionResult GetWithPagination(int pageNumber, int pageSize)
        {
            var result = ApplicationService.ReadAll(_ => true).OrderByDescending(x => x.Id)
                .Skip(pageNumber * pageSize)
                .Take(pageSize).ToList();

            var resultViewModel = Mapper.Map<List<TIndexViewModel>>(result);

            return Ok(resultViewModel);
        }

        /// <summary>
        ///     Get the elements with pagination and sorting
        /// </summary>
        /// <param name="pageNumber">The number of the current page</param>
        /// <param name="pageSize">The amount of elements per page</param>
        /// <param name="columnNameForSorting">The name of the column for sorting</param>
        /// <param name="sortingType">The type of sorting, possible values: ASC and DESC</param>
        /// <param name="columnsToReturn">The name of the columns to return</param>
        /// <response code="200">When the entity is found by its id</response>
        [HttpGet()]
        public virtual IActionResult GetWithPaginationAndFilter(int pageNumber, int pageSize, string columnNameForSorting, string sortingType, string columnsToReturn = "*")
        {
            var tableName = this.ControllerContext.ActionDescriptor.ControllerName;
            var result = ApplicationService.Get(pageNumber, pageSize, columnNameForSorting, sortingType, columnsToReturn, tableName);
            var resultViewModel = Mapper.Map<IEnumerable<TIndexViewModel>>(result);

            return Ok(resultViewModel);
        }


        /// <summary>
        ///     Gets an entity by its ID
        /// </summary>
        /// <param name="objectId">The id of the entity to return</param>
        /// <returns>The entity with ID=<paramref name="id"/>, null if not found</returns>
        /// <response code="200">When the entity is found by its id</response>
        /// <response code="404">When the entity couldn't be found</response>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey objectId)
        {
            var result = await ApplicationService.SingleOrDefaultAsync(objectId);

            if (result == null)
                return NotFound();
            var resultViewModel = Mapper.Map<TIndexViewModel>(result);

            return Ok(resultViewModel);
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="newObject">The new entity</param>
        /// <returns>The created entity</returns>
        /// <response code="201">When the entity was successfully created</response>
        /// <response code="400">When the entity model was not in a correct state and validation failed</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> Post([FromBody, Required]TCreateViewModel newObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity newEntity;
            try
            {
                newEntity = Mapper.Map<TCreateViewModel, TEntity>(newObject);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            newEntity = await ApplicationService.AddAsync(newEntity);
            await ApplicationService.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = newEntity.Id }, Mapper.Map<TEntity, TIndexViewModel>(newEntity));
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="id">The id of the entity to update</param>
        /// <param name="value">The updated entity</param>
        /// <returns>The updated entity</returns>
        /// <response code="200">When the entity was successfully updated</response>
        /// <response code="400">When the entity model was not in a correct state and validation failed</response>
        /// <response code="404">When the entity to update was not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> Put(TKey id, [FromBody]TEditViewModel value)
        {
            var originalEntity = await ApplicationService.SingleOrDefaultAsync(id);

            if (originalEntity == null)
                return NotFound(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = Mapper.Map(value, originalEntity);

            entity = await ApplicationService.UpdateAsync(entity);
            await ApplicationService.SaveChangesAsync();

            return Ok(Mapper.Map<TEntity, TIndexViewModel>(entity));
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        /// <returns>The deleted entity</returns>
        /// <response code="200">When the entity was successfully deleted</response>
        /// <response code="404">When the entity to delete was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            var entity = await ApplicationService.SingleOrDefaultAsync(id);
            if (entity != null)
            {
                await ApplicationService.RemoveAsync(entity);
                await ApplicationService.SaveChangesAsync();

                return Ok();
            }
            else return NotFound();
        }

    }
}