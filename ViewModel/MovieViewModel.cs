using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrettyWorld.ViewModel;

namespace PrettyWorld.ViewModel
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime WatchDate { get; set; }
        public string? MovieType { get; set; }
        public IFormFile MoviePicture { get; set; } = null!;
        public string? Trailer { get; set; }
        public string? Director { get; set; }
        public string? TopCast { get; set; }
        public string? Review { get; set; }

        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal? Rating { get; set; }
        public int? Plot { get; set; }
        public int? Scene { get; set; }
        public int? Sound { get; set; }
        public int? Immersion { get; set; }
        public int? Acting { get; set; }
    }
}
