-- 1)
SELECT * FROM orders 
	WHERE customer_ID = 123456
	ORDER BY order_date DESC;


-- 2)
SELECT cust.customer_ID, Max(order_date) AS mostRecentOrderDate
FROM customer cust
LEFT JOIN orders o
	ON cust.customer_ID = o.customer_ID 
GROUP BY cust.customer_ID 


-- 3)
SELECT sum(op.quantity) as unitsSold
FROM order_product op
JOIN product p
	ON p.product_number LIKE op.product_number
JOIN orders o
	ON o.order_ID = op.order_ID
WHERE DateDiff(MONTH, o.order_date, getDate()) < 12

-- 4)
SELECT TOP 3
SUM(p.base_price*op.quantity) AS revenue, CAST(op.product_number AS Nvarchar(MAX)) AS product_number
FROM orders o
JOIN order_product op
	ON o.order_ID = op.order_ID
JOIN product p 
	ON op.product_number LIKE p.product_number 
WHERE DateDIFF(DAY, order_date, getDate()) < 30
GROUP BY CAST(op.product_number AS Nvarchar(MAX))
ORDER BY revenue DESC

-- 5)
SELECT a.order_ID, CASE WHEN a.charged = a.shouldCharge THEN 1 ELSE 0 END AS chargedCorrectly
FROM
(
SELECT o.order_ID
	, MAX(order_total_amount) AS charged
	, SUM(p.base_price * op.quantity) * 1.1 + 5 * COUNT(DISTINCT(CAST(p.product_number AS NVARCHAR(MAX)))) AS shouldCharge
FROM orders o
JOIN order_product op
	ON o.order_ID = op.order_ID
JOIN product p 
	ON p.product_number like op.product_number
GROUP BY o.order_ID
) a


-- 6)
/* 
- product table: take out weight in inches
- add product name to product table
- add starting date to promotion table
- change data type text to varchar 
- don't store plain text password, card_number, card_security_code in database, instead use a hash 
- turn payment_type into a key pointing at new table for payment_types and corresponding expression
- equally, turn user_account_status into a key pointing at a new table for status and status_expresion 
- change name of 'order' to e.g. 'orders', since the word 'order' is reserved in SQL
- do a check constraint instead of number 50
*/



