namespace server.Models;

public class Product
{
    public int Id { get; set; }           // 自動變成主鍵 (Primary Key)
    public string Name { get; set; } = ""; // 菜名
    public decimal Price { get; set; }    // 價格
    public string? Description { get; set; } // 描述 (可為空)
    public string? Category { get; set; }    // 分類 (例如：主食、飲料)
}