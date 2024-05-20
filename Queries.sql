-- Задание 1, Вариант 1
-- Пункт 1
SELECT 
    Sellers.Surname, 
    Sellers.Name, 
    SUM(Sales.Quantity) AS SalesQuantity
FROM 
    Sales
LEFT JOIN 
    Sellers ON Sales.IDSel = Sellers.ID
WHERE 
    Sales.Date BETWEEN '2013-10-01' AND '2013-10-07'
GROUP BY 
    Sellers.Surname, Sellers.Name
ORDER BY 
    Sellers.Surname, Sellers.Name


-- Пункт 2
WITH TotalProductSales AS (
    SELECT
        s.IDProd,
        SUM(s.Quantity) AS SalesQuantity
    FROM
        Sales s
    JOIN
        Products AS p ON s.IDProd = p.ID
    WHERE
        s.Date BETWEEN '2013-10-01' AND '2013-10-07'
    GROUP BY
        s.IDProd
),
FilteredProducts AS (
    SELECT DISTINCT
        a.IDProd
    FROM
        Arrivals AS a
    WHERE
        a.Date BETWEEN '2013-09-07' AND '2013-10-07'
)
SELECT
    p.Name AS ProductName,
    e.Surname,
    e.Name,
    (CAST(SUM(s.Quantity) AS FLOAT) / tps.SalesQuantity) * 100 AS SalesPercentage
FROM
    Sales s
LEFT JOIN
    Sellers AS e ON s.IDSel = e.ID
LEFT JOIN
    Products AS p ON s.IDProd = p.ID
INNER JOIN
    TotalProductSales AS tps ON s.IDProd = tps.IDProd
INNER JOIN
    FilteredProducts AS fp ON s.IDProd = fp.IDProd
WHERE
    s.Date BETWEEN '2013-10-01' AND '2013-10-07'
GROUP BY
    p.Name,
    e.Surname,
    e.Name,
    tps.SalesQuantity
ORDER BY
    p.Name,
    e.Surname,
    e.Name