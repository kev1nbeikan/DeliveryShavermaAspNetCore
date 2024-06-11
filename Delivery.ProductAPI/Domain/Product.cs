using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.ProductAPI.Domain;

[Table("product", Schema = "public")]
public class Product
{
	[Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	
	[Column("title", TypeName = "varchar(32)")]
	public string Title { get; set; }
	
	[Column("description", TypeName = "varchar(256)"), Required]
	public string Description { get; set; }
	
	[Column("composition", TypeName = "varchar(128)"), Required]
	public string Composition { get; set; }
	
	[Column("price"), Required]
	public int Price { get; set; }
	
	[Column("image_path"), Required]
	public string ImagePath { get; set; }
}