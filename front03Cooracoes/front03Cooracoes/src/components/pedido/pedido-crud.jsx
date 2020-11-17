import React from 'react'
import Main from '../template/main'
import JwModal from 'jw-react-modal';
import Select from 'react-select';

import axios from 'axios'

const headerProps = {
    icon: 'users',
    title: 'pedidos',
    subtitle: 'Cadastro de pedidos'
}

const options = [
    { value: 'Cibbalena', label: 'Cibalena' },
    { value: 'Dorl', label: 'Doril' }    
  ];

const baseUrl = 'https://localhost:44375/api/pedido'
const initState= {    
    pedido: { numPedido: '', dataPedido:'', valorPedido: '', idClienteCpf:''},
    itempedido: { iditem: '', idpedido:'', qtdeproduto:''},
    produto: { idproduto: '', nomeproduto:'', vlrproduto:''},    
    list: [], itensPedido : [],
}

export default class PedidoCrud extends React.Component{

    state = { ...initState }

    /**Chamada quando o elemento for exibido na tela */
    componentWillMount() {
        axios.get(baseUrl,{           
            crossdomain: true
        })
        .then(resp => {
            this.setState({ list: resp.data })/**salvamos dentro da lista as requisições */
        })        
    }


    /*Limpar formulario */
    clear() {
        this.setState({ user: initState.user })
    }

    insereItem(produto){
        alert(produto.nomeproduto + ' inserido com sucesso !');
        this.state.itensPedido.push(produto);
    }

    save() {
        const produto = this.state.produto        
        const method = produto.idproduto ? 'put' : 'post'
        const url = produto.id ? `${baseUrl}/${produto.id}` : baseUrl
        var config = {
            headers: {crossdomain: true}
        };
        axios[method](url,produto, config)
        .then(resp => {
            const list = this.getUpdatedList(resp.data)
            this.setState({ produto: initState.produto, list })  
            console.log(resp.data)         
        })
        .catch(error => {
            console.log(error)
        })

    }
    getUpdatedList(produto){       
        const list = this.state.list.filter(u => u.id !== produto.idproduto) /**removendo o usuario da lista */
        list.unshift(produto) /**inserindo na primeira posição do array */
        return list
    }

    updatefield(event) {
        alert('vou inserir');
        //const produto = { ...this.state.produto }
        //produto[event.target.name] = event.target.value /**em target pegamos o conteúdo de input name */
        //this.setState({ produto })
        this.setState({ qtdeproduto: this.state.qtdeproduto })
    }
    
    /**edição */
    load(pedido){
        this.setState({ pedido })/**atualiza o estado da aplicação. */
    }
    remove(pedido){
        axios.delete(`${baseUrl}/${pedido.idpedido}`)
        .then(resp => {
            const list = this.state.list.filter(u => u !== pedido)
            this.setState({ list })
        })
    }

    /**list users */
    rendertable(){        
        return(
            <table className="table mt-4">
               <thead>                             
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th>
                            <button onClick={JwModal.open('jw-modal-pedido')} className="btn btn-success">Novo Pedido</button> 
                        </th>
                    </tr>
                    
                    <tr>
                        <th>ID</th>
                        <th>Nome</th>
                        <th>Qtde</th>
                        <th>Editar</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderows()}
                </tbody>            
                <JwModal id="jw-modal-pedido">
                <div className="row">  
                  <h1>Pedido</h1>  
                </div>  
                <div className="row">
                     <div className="col-12 col-md-6">
                           <div className="form-group">
                                <label htmlFor="name">Pedido</label>
                                <input type="text" className="form-control" 
                                    name="numped" 
                                    value={this.state.pedido.numped}
                                    onChange={e => this.updatefield(e)}
                                    placeholder="Digite o Pedido."
                                    />
                            </div>
                     </div>    
                     <div className="col-12 col-md-6">
                            <div className="form-group">
                                <label htmlFor="name">Data</label>
                                <input type="text" className="form-control" 
                                    name="datapedido" 
                                    value={this.state.pedido.datapedido}
                                    onChange={e => this.updatefield(e)}
                                    placeholder="Digite a data.."
                                    />
                            </div>
                     </div>    
                </div>    
                <div className="row">
                     <div className="col-12 col-md-6">
                        <div className="form-group">
                            <label htmlFor="cpf">Dados do Cliente</label>
                            <input type="cpf" className="form-control" 
                                name="cpf" 
                                value={this.state.pedido.cpfcliente}
                                onChange={e => this.updatefield(e)}
                                placeholder="Digite o cpf.."
                                />
                        </div>
                     </div>
                </div>   
                <div className="row">
                  <div className="col-12 col-md-6">
                        <div className="form-group">
                            <label htmlFor="nome"></label>                            
                        </div>
                  </div>
                </div>                                
                <div className="row"><br></br><br></br>
                   <div className="col-12 col-md-4">    
                     <div className="form-group">                            
                         <Select options = {options} />                        
                     </div>
                   </div>
                   <div className="col-12 col-md-4">       
                     <div className="form-group">                        
                         <input type="qtde" className="form-control" 
                             name="qtde" 
                             value={this.state.itempedido.qtdeproduto}
                             onChange={e => this.updatefield(e)}
                             placeholder="Digite a qtde.."
                            />
                     </div>
                   </div>
                   <div className="col-12 col-md-2"></div>       
                   <div className="col-12 col-md-2">       
                      <div className="col-12 d-flex justify-content end">
                            <button className="btn btn-primary"
                            onClick={this.insereItem(this.state.produto)}>Inserir</button>                             
                      </div>
                   </div>                            
                 </div>
                 <div className="row">

                   <table className="table mt-4">
                     <thead>                                                                     
                       <tr>
                         <th>ID</th>
                         <th>Nome</th>
                         <th>Qtde</th>
                         <th>Editar</th>
                       </tr>
                     </thead>
                     <tbody>
                       {this.renderows()}
                     </tbody>                                                      
                   </table>
                 </div>
                 <div className="row">  
                       <button className="btn btn-primary" onClick={e => this.save(e)}>Salvar</button>
                       <button className="btn btn-secondary ml-2" onClick={JwModal.close('jw-modal-pedido')}>Fechar</button>                     
                 </div>                 
               </JwModal>
            </table>            
        );
    }

    renderows(){        
        alert('inserir o produto' + this.state.itempedido.idproduto); 
        /**mapeando usuários que estão no estado do objeto */
        return this.state.itensPedido.map((itempedido, index) => {
            return (                
                <tr key={index}>
                    <td>{itempedido.iditem}</td>
                    <td>{itempedido.idproduto}</td>
                    <td>{itempedido.qtdeproduto}</td>
                    <td>
                        <button className="btn btn-warning mr-2"
                        onClick={() => this.load(itempedido)}>
                            <i className="fa fa-pencil"></i>
                        </button>
                        <button className="btn btn-danger"
                        onClick={() => this.remove(itempedido)}>
                            <i className="fa fa-trash"></i>
                        </button>
                    </td>
                </tr>
            );
        })
    }    

    render(){        
        return(            
            <Main {...headerProps}>
                    
                {this.rendertable()}

            </Main>
        );
    }
}