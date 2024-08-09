using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TikiCategoryDto
    {
        public TikiCategoryDto()
        {
            tikiCategoryDtos = new List<TikiCategoryDto>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public List<TikiCategoryDto> tikiCategoryDtos { get; set; }

    }
}