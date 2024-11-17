-- Total transactions for a month
SELECT
    SUM(CASE
        WHEN indicator = 'Debit' THEN -amount
        WHEN indicator = 'Credit' THEN amount
    END) AS net_total
FROM SavedTransactions
WHERE strftime('%m', booking_date) = '09'
AND strftime('%Y', booking_date) = '2024';

-- Total transaction for each month
SELECT
    strftime('%Y-%m', booking_date) AS month,
    SUM(CASE
            WHEN indicator = 'Debit' THEN -amount
            WHEN indicator = 'Credit' THEN amount
        END) AS net_total
FROM SavedTransactions
GROUP BY strftime('%Y-%m', booking_date)
ORDER BY month;

-- Total all time
SELECT
    SUM(CASE
            WHEN indicator = 'Debit' THEN -amount
            WHEN indicator = 'Credit' THEN amount
        END) AS net_total
FROM SavedTransactions;

-- All transactions for a month
SELECT *
FROM SavedTransactions
WHERE strftime('%m', booking_date) = '09'
  AND strftime('%Y', booking_date) = '2024';

SELECT *
FROM SavedTransactions
WHERE strftime('%m', booking_date) = '09'
  AND strftime('%Y', booking_date) = '2024'
  AND indicator = 'Debit';

SELECT
    SUM(-amount)
FROM SavedTransactions
WHERE indicator = 'Debit'
  AND strftime('%m', booking_date) = '09'
  AND strftime('%Y', booking_date) = '2024';

SELECT * FROM SavedTransactions;