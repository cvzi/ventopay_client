# Ventopay SOAP WSDL test client
Test client for https://appservice.ventopay.com/

Most of the program is auto-generated from https://appservice.ventopay.com/ventopayappservice.svc via
```bash
svcutil.exe "https://appservice.ventopay.com/ventopayappservice.svc?wsdl"
```

<details>
  <summary>Program Output</summary>

  ```text

Creating Client: VentopayMobileServiceClient -> Ok
VpMob_GetRestaurant() -> Ok

Restaurants:

ventopay gmbh(Hagenberg im Mühlkreis)
Restaurant_ID:    1239ce0b-a0c5-47ec-aa3b-bf1919cd6a0c
ID:               55
RestaurantNumber: 11
Modules:          * MODULE_HOME
                  * MODULE_SALDO
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_PAYMENT
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_BONUS
                  * MODULE_MENUPLAN
                  * MODULE_CUSTOMLINK
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_NEWS
                  * MODULE_CUSTOMLINK
                  * MODULE_FEEDBACK
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_EVENTREGISTRATION
                  * MODULE_EVENTADMINISTRATION
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_CONTACT
                  * MODULE_CUSTOMLINK
                  * MODULE_JOBS
                  * MODULE_SETTINGS
                  
-------------------

Österreichisches Bundesheer(Wien)
Restaurant_ID:    0d683283-815f-41a9-aa9c-8d595fa44c94
ID:               59
RestaurantNumber: 3
Modules:          * MODULE_HOME
                  * MODULE_SALDO
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_PAYMENT
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_BONUS
                  * MODULE_MENUPLAN
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_NEWS
                  * MODULE_CUSTOMLINK
                  * MODULE_FEEDBACK
                  * MODULE_EVENTREGISTRATION
                  * MODULE_EVENTADMINISTRATION
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_CONTACT
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_JOBS
                  * MODULE_SETTINGS

-------------------

EPIC TASTE(Osnabrück)
Restaurant_ID:    641e8371-d759-4977-8de3-524084f0061c
ID:               60
RestaurantNumber: 2021
Modules:          * MODULE_HOME
                  * MODULE_SALDO
                  * MODULE_PAYMENT
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_CUSTOMLINK
                  * MODULE_BONUS
                  * MODULE_MENUPLAN
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_NEWS
                  * MODULE_FEEDBACK
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_JOBS
                  * MODULE_CONTACT
                  * MODULE_SETTINGS

-------------------

apetito AG(Rheine)
Restaurant_ID:    c5612798-1995-42ca-8a4c-795b85b266e8
ID:               61
RestaurantNumber: 101
Modules:          * MODULE_CUSTOMLINK
                  * MODULE_HOME
                  * MODULE_PAYMENT
                  * MODULE_PREVIOUS_BONS
                  * MODULE_CUSTOMLINK
                  * MODULE_SETTINGS
                  * MODULE_FEEDBACK

-------------------

Lufthansa(Frankfurt/Main)
Restaurant_ID:    e57c4592-02e4-4088-a44c-11b1a4d43b1d
ID:               62
RestaurantNumber: 20
Modules:          * MODULE_HOME
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_SALDO
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_PAYMENT
                  * MODULE_CUSTOMLINK
                  * MODULE_BONUS
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_CUSTOMLINK
                  * MODULE_FEEDBACK
                  * MODULE_EVENTREGISTRATION
                  * MODULE_EVENTADMINISTRATION
                  * MODULE_CONTACT
                  * MODULE_CUSTOMLINK
                  * MODULE_JOBS
                  * MODULE_SETTINGS

-------------------

Dr. Ing. h.c. F. Porsche Aktiengesellschaft(Stuttgart)
Restaurant_ID:    a7613c8f-19b7-4667-8682-18784067c2fe
ID:               68
RestaurantNumber: 1
Modules:          * MODULE_HOME
                  * MODULE_SALDO
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_PAYMENT
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_BONUS
                  * MODULE_MENUPLAN
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_NEWS
                  * MODULE_CUSTOMLINK
                  * MODULE_FEEDBACK
                  * MODULE_EVENTREGISTRATION
                  * MODULE_EVENTADMINISTRATION
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_CONTACT
                  * MODULE_CUSTOMLINK
                  * MODULE_JOBS
                  * MODULE_SETTINGS
                  * Schulungsvideos

-------------------

LASK(Pasching)
Restaurant_ID:    f73f40fe-766d-4521-a7ea-84242bcd09ff
ID:               72
RestaurantNumber: 1908
Modules:          * MODULE_HOME
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_CUSTOMLINK
                  * MODULE_CHARGE_CARD
                  * MODULE_BONUS
                  * MODULE_SALDO
                  * MODULE_PREVIOUS_BONS
                  * MODULE_PAYMENT
                  * MODULE_FEEDBACK
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_CUSTOMLINK
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_NUTRIENT_INFORMATION
                  * MODULE_CONTACT
                  * MODULE_SETTINGS

-------------------

Roche Casino(Mannheim)
Restaurant_ID:    82e2acbb-3a1f-4817-99ba-12dd86ba88e3
ID:               73
RestaurantNumber: 2000
Modules:          * MODULE_HOME
                  * MODULE_BONUS
                  * MODULE_PAYMENT
                  * MODULE_SALDO
                  * MODULE_PREVIOUS_BONS
                  * MODULE_MENUPLAN
                  * MODULE_CHAIRCOVERAGE
                  * MODULE_FEEDBACK
                  * MODULE_CONTACT
                  * MODULE_SETTINGS

-------------------

Schwarz Hirsch(Wien)
Restaurant_ID:    1c413044-8fe9-41ae-bc52-012137fa79fb
ID:               74
RestaurantNumber: 4001
Modules:          * MODULE_HOME
                  * MODULE_MENUPLAN
                  * MODULE_BONUS
                  * MODULE_CHARGE_CARD
                  * MODULE_PREVIOUS_BONS
                  * MODULE_SALDO
                  * MODULE_PAYMENT
                  * MODULE_CONTACT
                  * MODULE_SETTINGS

-------------------

VpMob_GetMenu("a7613c8f-19b7-4667-8682-18784067c2fe") -> Ok

PDF with size 453KB
Writing PDF to bin\Release\out.pdf -> Ok!

VpMob_GetMenu("0d683283-815f-41a9-aa9c-8d595fa44c94") -> Ok

URL: "http://menuplan.eurest.at/menu.html?current_url=%2FCurrentWeek%2FK19910_DEU.xml&showAdditives=0&showMealColors=0"


Press any key to exit...

  ```

</details>
