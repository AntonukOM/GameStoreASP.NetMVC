-- in project GameStore was used Entity Framework code first method 
use GameStore;

SELECT * FROM Games;
INSERT INTO Games(Name, Description, Category, Price) 
VALUES 
	('SimCity', '����������������� ��������� ����� � ����! �������� ����� ����� �����', '���������', 149.00),
	('TITANFALL', '��� ���� ��������� ��� �� ���������, ��� ����� ������������������� ��������, ������� � ���������, � ������� � ������', '�����', 229.00),
	('Battlefield 4', 'Battlefield 4 � ��� ������������ ��� �����, ������ ������ ������, ��������� ����� ��������������, ������ ������� ���', '�����', 89.40),
	('The Sims 4', '� ���������� ������� �������� ���� ������� ���� ���� �����. �� � ������� The Sims 4 ��� ����������� ����� �����! 
		��� ������ � ���, ��� � � ��� ����, ��� ����������, ��� �������� � ������������ ���� ���', '���������', 15.00),
	('Dark Souls 2', '����������� ����������� �������� ������ ����� �������� ������� ������ ����� ���������� ���������. Dark Souls II ��������� 
		������ �����, ����� ������� � ����� ���. ���� ���� ��������� � ������ � ������� ��������� Dark Souls ����� ��������.', 'RPG', 99.00),
	('The Elder Scrolls V: Skyrim', '����� �������� ������ �������� ������� ��������� �� ����� ����������. ������ ������������ �� ������� ���������� ����� �����, 
		� ���������� ��������. � ���� ��, ��� ������������� ������� ������, � ��� ��������� �������� � ����������� �������. ������ ������� �������� � ���� 
		������� ������� �� ����������������� � ��������, � ����� �������� ����� ����� ����������� �������.', 'RPG', 139.00),
	('FIFA 14', '�����������, ��������, ������������� ������! ����������� �������� �������� FIFA ���� ��� ����� ��������� ����������, ���������� ���������� ���� �
		 ������ ���� � ����������� �������� � ����.', '���������', 699.00),
	('Need for Speed Rivals', '�������� ��� ����������� ������ ����. ������� ����� ����� ��������� � ��������������������� ������� � ���������� ������������� 
		����� ��������� � ��������. �������� ������� � ���, � ������� ���� ������ ��� ��������� � ������ � �������. ', '���������', 15.00),
	('Crysis 3', '�������� ���� ��������������� � 2047 ����, � ��� ��������� ��������� � ���� �������.', '�����', 129.00),
	('Dead Space 3', '� Dead Space 3 ����� ����� � ������� ������ ���� ������ ������������ � ����������� �����������, ����� ������ � ������������� �����������.', '�����', 49.00);