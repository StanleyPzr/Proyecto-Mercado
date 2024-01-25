using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Comment;
using API.Models;

namespace API.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Titulo = commentModel.Titulo,
                Contenido = commentModel.Contenido,
                CreadoEn = commentModel.CreadoEn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDTO commentDTO, int stockId)
        {
            return new Comment
            {                
                Titulo = commentDTO.Titulo,
                Contenido = commentDTO.Contenido,
                StockId = stockId               
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDTO)
        {
            return new Comment
            {                
                Titulo = commentDTO.Titulo,
                Contenido = commentDTO.Contenido
                             
            };
        }
    }
}