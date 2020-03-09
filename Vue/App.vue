<template>
<div id="app">
  <div id="buttons">
    <button @click="showInput(1)">Listar cidades</button>&nbsp;
    <button @click="showInput(2)">Cadastrar cidades</button>&nbsp;
    <button @click="showInput(3)">Relatório por Quantidade UF</button>&nbsp;
    <button @click="showInput(4)">Relatório por Quantidade de Regiões</button>&nbsp;
    <button @click="showInput(5)">Procurar Cidades</button>&nbsp;
  </div>
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <div id="cadastra-cidade" class="cadastro" v-show="showCadastraCidades">
    <input type="text" placeholder="ibge"   v-model="ibge">
    <input type="text" placeholder="uf"   v-model="uf">
    <input type="text" placeholder="nome"   v-model="nome">
    <input type="text" placeholder="longitude"   v-model="longitude">
    <input type="text" placeholder="latitude"   v-model="latitude">
    <input type="text" placeholder="regiao"   v-model="regiao">
    <input type="submit" value="Cadastrar" @click="addCidade()">
  </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

  <div id="filtrar-cidade" v-show="showFiltrar" style="margin-top:25px;">
    <div>
    <input type="text" v-model="procurarPorNome" placeholder="Nome"/>&nbsp;&nbsp;
    <input type="text" v-model="procurarPorUf" placeholder="UF"/>&nbsp;&nbsp;
    <input type="text" v-model="procurarPorRegiao" placeholder="Região"/>&nbsp;&nbsp;
        <label>Procurar:</label>
  </div>
  <div >
    <div v-for="cidade in filteredList" :key="cidade.id">
      <!-- <small>posted by: {{ cidade.nome }}</small>
        {{ cidade.uf }}-->
     <table border="1px" style="margin-top:30px; margin-left: 500px;">
      <thead>
        <th>ID</th>
        <th>IBGE</th>
        <th>UF</th>
        <th>Cidade</th>
        <th>Longitude</th>
        <th>Latitude</th>
        <th>Região</th>
        <th>Remover</th>
      </thead>
      <tbody>
          <tr>
          <td>{{cidade.id}}</td>
          <td>{{cidade.ibge}}</td>
          <td>{{cidade.uf}}</td>
          <td>{{cidade.nome}}</td>
          <td>{{cidade.longitude}}</td>
          <td>{{cidade.latitude}}</td>
          <td>{{cidade.regiao}}</td>
          <td>
            <button class="btn" @click="removerCidade(cidade)">Remover</button>
            </td>
        </tr>
      </tbody>
    </table>
    </div>
      
    </div>
  </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

  <div id="lista-cidades" style="padding-top : 25px" v-show="showListaCidades">
    <table border="1px">
      <thead>
        <th>ID</th>
        <th>IBGE</th>
        <th>UF</th>
        <th>Cidade</th>
        <th>Longitude</th>
        <th>Latitude</th>
        <th>Região</th>
        <th>Remover</th>
      </thead>
      <tbody>
        <!--<tr v-for="(cidade, index) in  cidades " :key="index">-->
         <tr v-for="cidade in cidades " :key="cidade.id">
          <td>{{cidade.id}}</td>
          <td>{{cidade.ibge}}</td>
          <td>{{cidade.uf}}</td>
          <td>{{cidade.nome}}</td>
          <td>{{cidade.longitude}}</td>
          <td>{{cidade.latitude}}</td>
          <td>{{cidade.regiao}}</td>
          <td>
            <button class="btn" @click="removerCidade(cidade)">Remover</button>
            </td>
        </tr>
      </tbody>
    </table>
  </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  
   <div id="relatorio" v-show="showRelatorioPorUf" style="margin-top:25px;">
    <ul v-for="(cidade, index) in quantidadeCidadesPorUf " :key="index">
      <li>
        <label> UF: {{cidade.uf}}</label>
        <label> Quantidade : {{cidade.quantidadeCidadesPorUf}}</label>
        </li>

    </ul>

  </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <div id="relatorio" v-show="showRelatorioPorRegiao" style="margin-top:25px;">
    <ul v-for="(cidade, index) in quantidadeCidadesPorRegiao " :key="index">
      <li>
        <label> Região: {{cidade.regiao}}</label>
        <label> Quantidade : {{cidade.quantidadeCidadesPorRegiao}}</label>
        </li>

    </ul>

  </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

  


</div>
</template>
   
 <!--
    
    </div>-->


<script>


export default {
        name : 'app',
        
       data(){
     return { 
       cidades : [],
       quantidadeCidadesPorUf : [],
       quantidadeCidadesPorRegiao : [],
       ibge : "",
       uf : "",
       nome : "",
       latitude : "",
       longitude : "",
       regiao : "",
       procurarPorNome : "",
       procurarPorRegiao : "",
       procurarPorUf : "",
       showListaCidades : false,
       showCadastraCidades : false,
       showFiltrar : false,
       showRelatorioPorUf : false,
       showRelatorioPorRegiao : false
   }
  },
  computed: {
    filteredList() {
      return this.cidades.filter(cidade => {
        if (this.procurarPorNome != ""){
          return cidade.nome.includes(this.procurarPorNome)
        }
        else if (this.procurarPorRegiao != ""){
             return cidade.regiao.includes(this.procurarPorRegiao)
        }
        else if (this.procurarPorUf != ""){
           return cidade.uf.includes(this.procurarPorUf)
        }

      })
    }
  },
   


  created(){
   
   this.$http
   .get('https://localhost:44378/api/Cidades')
   .then(res => res.json())
   .then(cidades => this.cidades = cidades )
   

  },
   methods : {
    addCidade() {

      if (this.nome == ""){
        alert("Preencha o campo nome!")
      }
      else if (this.ibge == ""){
        alert("Preencha o campo ibge!")
      }
      else if (this.uf == ""){
        alert("Preencha o campo uf")
      }

      else if (this.latitude == ""){
        alert("Preencha o campo latitude")
      }

      else if (this.longitude == ""){
        alert("Preencha o campo longitude")
      }

      else if (this.regiao == ""){
        alert("Preencha o campo regiao")
      }
      else{

        let _cidade = {
          ibge : this.ibge,
          uf : this.uf,
          nome : this.nome,
          longitude : this.longitude,
          latitude : this.latitude,
          regiao : this.regiao,
        
        }
         this.$http
        .post('https://localhost:44378/api/Cidades', _cidade)
        .then(res => res.json())
        .then(cidade => {
          console.log(cidade);
          if (cidade.error != "" ){
            alert(cidade.error);
          }
          else{
          this.cidades.push(cidade)
          this.ibge = "";
          this.uf = "";
          this.nome = "";
          this.longitude = "";
          this.latitude = "";
          this.regiao = "";
          }
        })
        
     

 
      }
    },
    
    showInput(number){
     if (number == 1){
     this.showListaCidades = true;
     if (this.showCadastraCidades) {
       this.showCadastraCidades = false;
     }
    else if (this.showRelatorioPorUf){
      this.showRelatorioPorUf = false;
    }   
    else if (this.showRelatorioPorRegiao){
      this.showRelatorioPorRegiao = false;
    }

    else if (this.showFiltrar){
        this.showFiltrar = false;
      }
       
   }
    else if (number == 2){
      this.showCadastraCidades = true;
      if (this.showListaCidades){
         this.showListaCidades = false; 
      }
      else if (this.showRelatorioPorUf){
      this.showRelatorioPorUf = false;
    }   
    else if (this.showRelatorioPorRegiao){
      this.showRelatorioPorRegiao = false;
      }
    else if (this.showFiltrar){
        this.showFiltrar = false;
      }
    }

    else if (number == 3){
      this.showRelatorioPorUf = true;
      if (this.showListaCidades){
        this.showListaCidades = false;
      }
      else if (this.showCadastraCidades){
        this.showCadastraCidades = false;
      }
       else if (this.showRelatorioPorRegiao){
        this.showRelatorioPorRegiao = false;
      }
      else if (this.showFiltrar){
        this.showFiltrar = false;
      }
   this.$http
   .get('https://localhost:44378/api/Cidades/reportUf')
   .then(res => res.json())
   .then(quantidadeCidadesPorUf => this.quantidadeCidadesPorUf = quantidadeCidadesPorUf)
   
  }


  else if (number == 4){
      this.showRelatorioPorRegiao = true;
      if (this.showListaCidades){
        this.showListaCidades = false;
      }
      else if (this.showCadastraCidades){
        this.showCadastraCidades = false;
      }
      else if (this.showRelatorioPorUf){
        this.showRelatorioPorUf = false;
      }
      else if (this.showFiltrar){
        this.showFiltrar = false;
      }
   this.$http
   .get('https://localhost:44378/api/Cidades/reportRegiao')
   .then(res => res.json())
   .then(quantidadeCidadesPorRegiao => this.quantidadeCidadesPorRegiao = quantidadeCidadesPorRegiao)
   

  }


    else if (number == 5){
      this.showFiltrar = true;
      if (this.showListaCidades){
        this.showListaCidades = false;
      }
      else if (this.showCadastraCidades){
        this.showCadastraCidades = false;
      }

       else if (this.showCadastraCidades){
        this.showCadastraCidades = false;
      }
      else if (this.showRelatorioPorUf){
        this.showRelatorioPorUf = false;
      }
      else if (this.showRelatorioPorRegiao){
        this.showRelatorioPorRegiao = false;
      }
      
    }
  },

   


   
    
    }
  
  }

</script>

<style scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.lista-cidades{
  margin-top : 90px;
  margin-left : 400px;
}
.cadastro{
  margin-top: 30px;
}

</style>

  
  


