import React from 'react'
import Main from '../template/main'
import JwModal from 'jw-react-modal';

import axios from 'axios'

const headerProps = {
    icon: 'users',
    title: 'produtos',
    subtitle: 'Cadastro de produtos'
}

const baseUrl = 'https://localhost:44375/api/produto'
const initState= {
    produto: { idproduto:'', nomeproduto:'', valorproduto:''},
    list: []
}

export default class ProdutoCrud extends React.Component{

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
        this.setState({ produto: initState.produto })
    }
    save() {
        const produto = this.state.produto        
        const method = produto.idproduto ? 'put' : 'post'
        const url = produto.idproduto ? `${baseUrl}/${produto.idproduto}` : baseUrl
        var config = {
            headers: {crossdomain: true}
        };
        axios[method](url,produto,config)
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
        const produto = { ...this.state.produto }
        produto[event.target.name] = event.target.value /**em target pegamos o conteúdo de input name */
        this.setState({ produto })
    }


    /**edição */
    load(produto){
        this.setState({ produto })/**atualiza o estado da aplicação. */
    }
    remove(produto){
        axios.delete(`${baseUrl}/${produto.id}`)
        .then(resp => {
            const list = this.state.list.filter(u => u !== produto)
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
                            <button onClick={JwModal.open('jw-modal-produto')} className="btn btn-success">Novo Produto</button> 
                        </th>
                    </tr>
                    <tr>
                        <th>IdProduto</th>
                        <th>Nome</th>
                        <th>Valor</th>
                        <th>Editar</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderows()}
                </tbody>            
                <JwModal id="jw-modal-produto">
                  <div className="form">
                     <div className="row">  
                         <h1>Produtos</h1>  
                     </div>   
                     <div className="row">
                         <div className="col-12 col-md-6">
                            <div className="form-group">
                              <label htmlFor="name">IdProduto</label>
                              <input type="text" className="form-control" 
                                name="idproduto" 
                                value={this.state.produto.idproduto}
                                onChange={e => this.updatefield(e)}
                                placeholder="Digite o ID."
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="nome">Nome</label>
                                <input type="text" className="form-control" 
                                name="nome" 
                                value={this.state.produto.nome}
                                onChange={e => this.updatefield(e)}
                                placeholder="Digite o nome.."
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="valor">Valor</label>
                                <input type="text" className="form-control" 
                                   name="valor" 
                                   value={this.state.produto.valorproduto}
                                   onChange={e => this.updatefield(e)}
                                   placeholder="Digite o valor.."
                                  />
                            </div>
                         </div>
                  </div>
                  <hr />
                  <div className="row">
                    <div className="col-12 d-flex justify-content end">
                        <button className="btn btn-primary"
                        onClick={e => this.save(e)}>Salvar</button>
                        <button className="btn btn-secondary ml-2" onClick={JwModal.close('jw-modal-produto')}>Fechar</button>                        
                    </div>
                  </div>
                 </div>
                </JwModal>
            </table>
        );
    }
    renderows(){
        /**mapeando usuários que estão no estado do objeto */
        return this.state.list.map((produto,index) => {
            return (                
                <tr key={index}>
                    <td>{produto.idProduto}</td>
                    <td>{produto.nomeProduto}</td>
                    <td>{produto.valorProduto}</td>
                    <td>
                        <button className="btn btn-warning mr-2"
                        onClick={() => this.load(produto)}>
                            <i className="fa fa-pencil"></i>
                        </button>
                        <button className="btn btn-danger"
                        onClick={() => this.remove(produto)}>
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