using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Comment;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var comments = await _commentRepository.GetAllAsync();
            
            var CommentDTO = comments.Select(x => x.ToCommentDTO());

            return Ok(CommentDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null){
                return NotFound();
            }

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Crear([FromRoute] int stockId, CreateCommentDTO commentDTO)
        {
            if(!await _stockRepository.StockExists(stockId))
            {
                return NotFound();
            }
            var commentModel = commentDTO.ToCommentFromCreate(stockId);
            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDTO());  
                 
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDTO)
        {
            var comment = await _commentRepository.UpdateAsync(id, updateDTO.ToCommentFromUpdate());
            if (comment == null){
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            var commentModel = await _commentRepository.DeleteAsync(id);

            if (commentModel == null){
                return NotFound();
            }
            return Ok(commentModel);
        }
    }
}