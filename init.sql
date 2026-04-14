SET NAMES utf8mb4;

CREATE TABLE IF NOT EXISTS Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Description TEXT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    Category VARCHAR(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO Products (Name, Price, Description, Category) VALUES 
('經典美式', 80, '深焙豆萃取', 'Drink'),
('拿鐵', 120, '紐西蘭鮮乳', 'Drink'),
('牛肉漢堡', 180, '澳洲和牛', 'Food');