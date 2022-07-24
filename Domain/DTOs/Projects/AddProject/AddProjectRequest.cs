using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Projects.AddProject
{
    public class AddProjectRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
