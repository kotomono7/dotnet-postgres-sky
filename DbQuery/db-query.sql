CREATE EXTENSION dblink;

SELECT u.userid, u.name, u.nip, b.name, b.notlpn, b.city
FROM users AS u
LEFT JOIN (
	SELECT * FROM 
	dblink (
		'dbname=params port=5432 host=localhost user=postgres password=postgres', 
		'SELECT id, code, name, address, notlpn, city FROM public.branch'
	) 
	AS branch(
		id INTEGER, 
		code VARCHAR, 
		name VARCHAR, 
		address VARCHAR, 
		notlpn VARCHAR,
		city VARCHAR
	)
) AS b ON (u.branch = b.code);

-- SELECT * FROM users;

INSERT INTO users (userid, name, nip, branch, nohp, role)
	VALUES ('sky_test', 'Khoirul Umam', '22222233', 'ID0010028', '085741118200', 'IT');
	
UPDATE users SET nohp = '085741118205'
	WHERE userid = 'sky_test';
	
DELETE FROM users WHERE userid = 'sky_test';

INSERT INTO users (userid, name, nip, branch, nohp, role)
	VALUES ('sky_test', 'Khoirul Umam', '22222233', 'ID0010028', '085741118200', 'IT');
ON CONFLICT(userid)
DO UPDATE SET nohp = '085741118205';

SELECT dblink_exec(
	'host=localhost port=5432 dbname=params user=postgres password=postgres',
	'INSERT INTO branch (id, code, name, address, notlpn, city) 
		VALUES (default, ''ID0010029'', ''Cabang Pekalongan'', ''Jln pahlawan baru no 19'', ''021-0298288'', ''Pekalongan'' )'
);

SELECT * FROM 
dblink (
	'dbname=params port=5432 host=localhost user=postgres password=postgres', 
	'SELECT id, code, name, address, notlpn, city FROM public.branch'
) 
AS branch(
	id INTEGER, 
	code VARCHAR, 
	name VARCHAR, 
	address VARCHAR, 
	notlpn VARCHAR,
	city VARCHAR
);

SELECT u.userid, u.name, u.nip, 
	CASE 
		WHEN (u.userid = 'sky_zae') THEN 'Cabang Bandung'
		WHEN (u.userid = 'sky_dwi') THEN 'Cabang Jakarta' 
		WHEN (u.userid = 'sky_ius') THEN 'Cabang Semarang'
		ELSE '-'
	END AS branch
FROM users AS u;