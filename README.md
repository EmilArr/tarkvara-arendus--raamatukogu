# Tarkvaraarendus - raamatukogu rakendus
Rambo Raamatud™

## Funktsioonid
- raamatute vaatamine
  + raamatute haldamine (lisamine, eemaldamine ja kui jõuan siis andmete muutmine)
- kasutaja vaade
  + kasutajate haldamine (lisamine, eemaldamine ja kui jõuan siis andmete muutmine) 
-  Laenutus
  + Laenutamine ja tagastamine

## Andmebaas
- raamatute tabel, iga raamatu kohta:
  + väljaanne (isbn - vb saab kusagilt apist selle põhjal muid andmeid tõmmata, oleks kasulik raamatute lisamisel)
  + nimi
  + autor
  + id (kindlale füüsilisele raamatule unikaalne) - see ühendaks muude tabelitega
  + staatus (kohal/laenutatud)
  + tagastamiskuupäev (puudub/kp)
- kasutajate tabel
  + nimi
  + isikukood
  + koduaadress
  + laenutatud raamatud
  + laenutatud raamatute tähtajad
- neid ühendab nt. laenutatud raamatu id
