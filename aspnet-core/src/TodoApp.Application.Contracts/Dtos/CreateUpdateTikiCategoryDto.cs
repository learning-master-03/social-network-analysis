using System;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class CreateUpdateTikiCategoryDto 
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; } = true;

    }
}