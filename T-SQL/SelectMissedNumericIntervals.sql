declare @values as table
    (
        [number] int not null -- значение (уникальное значение в рамках таблицы)
    );


/* Тестовая ситуация */
insert into @values ([number])
values
(1), 
(2), 
(3), 
(5), 
(30),
(9),
(32);

/* Решение */
select l + 1 [left], r - 1 [right] from (
	select v.number as l, 
		(select top 1 [number] from @values where number > v.number order by number) as r	
	from @values v
) data
where r - l > 1
order by l
