import random
from faker import Faker
import sqlite3

conn = sqlite3.connect('pirlanta2.db')
c = conn.cursor()
fake = Faker()
Faker.seed(0)

s = ("ROUND","PRINCESS","OVAL","MARKIZ","PEAR","CUSHION","EMERALD","ASSCHER","RADIANT","HEART", "BAGET")
color = ("D","E","F","G","H","I","J","K","L","M","N","P","R","S","Z")
clarity = ("IF","VVS1","VVS2","VS1","VS2","SI1","SI2","SI3","I1","I2","I3")
cut = ("POOR","FAIR","GOOD","VERY GOOD","EXCELLENT")
cert = ("GIA","HRD")

round(random.uniform(0,4.00), 2)

"""
INSERT INTO "main"."Magazalar" ("Numara", "Adres") VALUES ('0531731624', 'mollafenari');
"""
a =5 
mail = "mail"
mq = (f"INSERT INTO \"main\".\"Magazalar\" (\"Numara\", \"Adres\", \"Name\") VALUES (\'{fake.phone_number()}\', \'{fake.address()}\', \'{fake.company()}\' );")
uq = (f"INSERT INTO \"main\".\"Users\" (\"Name\", \"Surname\", \"Mail\", \"MagazaId\") VALUES (\'{fake.first_name()}\', \'{fake.last_name()}\', \'{fake.simple_profile()[mail]}\', {int(round(random.uniform(1,150),0))});")
pq = (f"INSERT INTO \"main\".\"Pirlantalar\"(\"Adet\", \"Type\", \"Carat\", \"Color\", \"Clarity\", \"Cut\", \"Certficate\", \"MagazaId\") VALUES (\'{int(round(random.uniform(0,1000), 0))}\', '{random.choice(s)}', \'{round(random.uniform(0,4.00), 2)}\', '{random.choice(color)}', '{random.choice(clarity)}', '{random.choice(cut)}', '{random.choice(cert)}', {int(round(random.uniform(1,150),0))}));")
#for i in range(50): 
#    c.execute(f"INSERT INTO \"main\".\"Users\" (\"Name\", \"Surname\", \"Mail\" , \"MagazaId\") VALUES (\'{fake.first_name()}\', \'{fake.last_name()}\', \'{fake.simple_profile()[mail]}\', {int(round(random.uniform(1,50),0))});")


#for i in range(150): 
#    c.execute(f"INSERT INTO \"main\".\"Users\" (\"Name\", \"Surname\", \"Mail\" ) VALUES (\'{fake.first_name()}\', \'{fake.last_name()}\', \'{fake.simple_profile()[mail]}\');")
#for i in range(50):
#    c.execute(f"INSERT INTO \"main\".\"Magazalar\" (\"Numara\", \"Adres\", \"Name\", \"UserId\") VALUES (\'{fake.phone_number()}\', \'{fake.address()}\', \'{fake.company()}\',{int(round(random.uniform(1,200),0))}  );")


for i in range(10):
    c.execute(f"INSERT INTO \"main\".\"Pirlantalar\"(\"Adet\", \"Type\", \"Carat\",\"Price\", \"Color\", \"Clarity\", \"Cut\", \"Certficate\", \"MagazaId\") VALUES (\'{int(round(random.uniform(0,1000), 0))}\', '{random.choice(s)}', \'{round(random.uniform(0,4.00), 2)}\',10, '{random.choice(color)}', '{random.choice(clarity)}', '{random.choice(cut)}', '{random.choice(cert)}', 1);")
conn.commit()
conn.close()