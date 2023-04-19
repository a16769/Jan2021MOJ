import { Prilika } from "./Prilika.js";

export class Grad{

    //izmena za github 2.0

    constructor(id, naziv, longitude, latitude){
        this.id = id;
        this.naziv = naziv;
        this.longitude = longitude;
        this.latitude = latitude;
        this.Prilike = [];
        this.kontejner = null;
    }

    iscrtajGrad(){

        this.kontejner = document.createElement("div");
        this.kontejner.className = "grad";
        document.body.appendChild(this.kontejner);

        let kont = document.createElement("div");
        kont.className = "prikaz";
        this.kontejner.appendChild(kont);

        let meni = document.createElement("div");
        meni.className = "meniDiv";
        kont.appendChild(meni);
        this.iscrtajMeni(meni);

        let skale = document.createElement("div");
        skale.className = "skalePrikaz";
        kont.appendChild(skale);

        let skaleDiv = document.createElement("div");
        skaleDiv.className = "skaleDiv";
        skale.appendChild(skaleDiv);

    }

    iscrtajMeni(host){
        
        let nazivRed = document.createElement("div");
        nazivRed.className = "naziv";

        let x = this.latitude>0 ? this.latitude+"°N" : Math.abs(this.latitude+"°S");
        let y = this.longitude>0 ? this.longitude+"°E" : Math.abs(this.longitude+"°W");
        
        nazivRed.innerHTML = "Grad "+this.naziv+"("+x+", "+y+"), godina: 2023.";
        host.appendChild(nazivRed);

        let rbtnRed = document.createElement("div");
        rbtnRed.className = "rbtnDiv";

        let rbtnTemp = document.createElement("input");
        rbtnTemp.type = "radio";
        rbtnTemp.value = 1;
        rbtnTemp.name = this.naziv;
        let lblT = document.createElement("label");
        lblT.innerHTML = "Temperatura";
        rbtnRed.appendChild(rbtnTemp);
        rbtnRed.appendChild(lblT);

        let rbtnPad = document.createElement("input");
        rbtnPad.type = "radio";
        rbtnPad.value = 2;
        rbtnPad.name = this.naziv;
        let lblP = document.createElement("label");
        lblP.innerHTML = "Padavine";
        rbtnRed.appendChild(rbtnPad);
        rbtnRed.appendChild(lblP);

        let rbtnSD = document.createElement("input");
        rbtnSD.type = "radio";
        rbtnSD.value = 3;
        rbtnSD.name = this.naziv;
        let lblS = document.createElement("label");
        lblS.innerHTML = "Suncai dani";
        rbtnRed.appendChild(rbtnSD);
        rbtnRed.appendChild(lblS);
        
        host.appendChild(rbtnRed);

        let dugme = document.createElement("button");
        dugme.className = "dugme";
        dugme.name = this.naziv;
        dugme.innerHTML = "Prikazi";
        dugme.onclick= (ev)=> this.prikazi();
        host.appendChild(dugme);

    }


    iscrtajSkale(host){

        let skale = this.obrisiPrethodno();

        let selektovan = this.kontejner.querySelector("input[type=radio]:checked");
        if(selektovan === null){
            alert("Niste selektovali vremensku priliku!");
            return;
        }

        let radioValue = selektovan.value;

        let meseci = ["Jan", "Feb", "Mar", "Apr"];

        this.Prilike.forEach((p, index) => {
            let mesecVal = meseci[index];
            p.iscrtajPriliku(skale, radioValue, mesecVal);
        })

        host.appendChild(skale);
        
    }

    obrisiPrethodno(){

        let skaleDiv = document.querySelector(".skaleDiv");
        var roditelj = skaleDiv.parentNode;
        roditelj.removeChild(skaleDiv);

        skaleDiv = document.createElement("div");
        skaleDiv.className = "skaleDiv";
        roditelj.appendChild(skaleDiv);
        return skaleDiv;
    }

    prikazi(){

        let pomSkale = document.querySelector(".skalePrikaz");
        this.iscrtajSkale(pomSkale);
    }

}