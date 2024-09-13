using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.Models
{
    public class Image_produitModels
    {
        public string? Id_image { get; set; }
        public string? Id_produit {  get; set; }
        public byte[]? Image_couv {  get; set; }
        public byte[]? Image_1 {  get; set; }
        public byte[]? Image_2 {  get; set; }
        public byte[]? Image_3 {  get; set; }
        public byte[]? Image_4 {  get; set; }
        
    }
}
