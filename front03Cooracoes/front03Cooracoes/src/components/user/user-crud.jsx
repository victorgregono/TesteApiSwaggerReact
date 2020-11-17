import React from 'react'
import Main from '../template/main'
import JwModal from 'jw-react-modal';

import axios from 'axios'

const headerProps = {
    icon: 'users',
    title: 'Clientes',
    subtitle: 'Cadastro de Clientes'
}

//const baseUrl = 'http://localhost:8000/api/user'
const baseUrl = 'https://localhost:44375/api/Cliente'
const initState= {
    user: { IdClienteCpf:'', NomeCliente:'', Email:''},
    list: []
}

export default class UserCrud extends React.Component{

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
    save() {
        const user = this.state.user        
        const method = user.IdClienteCpf ? 'put' : 'post'
        const url = user.IdCienteCpf ? `${baseUrl}/${user.IdClienteCpf}` : baseUrl
        var config = {
            headers: {crossdomain: true}
        };
        axios[method](url,user,config)
        .then(resp => {
            const list = this.getUpdatedList(resp.data)
            this.setState({ user: initState.user, list })  
            console.log(resp.data)         
        })
        .catch(error => {
            console.log(error)
        })

    }
    getUpdatedList(user){       
        const list = this.state.list.filter(u => u.IdClienteCpf !== user.IdClienteCpf) /**removendo o usuario da lista */
        list.unshift(user) /**inserindo na primeira posição do array */
        return list
    }

    updatefield(event) {
        const user = { ...this.state.user }
        user[event.target.name] = event.target.value /**em target pegamos o conteúdo de input name */
        this.setState({ user })
    }

    /**edição */
    load(user){
        this.setState({ user })/**atualiza o estado da aplicação. */
    }
    remove(user){
        axios.delete(`${baseUrl}/${user.idClienteCpf}`)
        .then(resp => {
            const list = this.state.list.filter(u => u !== user)
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
                            <button onClick={JwModal.open('jw-modal-cliente')} className="btn btn-success">Novo Ciente</button> 
                        </th>
                    </tr>
                    <tr>
                        <th>Cpf</th>
                        <th>Nome</th>
                        <th>E-mail</th>
                        <th>Editar</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderows()}
                </tbody>       
                <JwModal id="jw-modal-cliente">                  
                     <div className="row">  
                         <h1>Clientes</h1>  
                     </div>   
                     <div className="form">
                        <div className="row">
                            <div className="col-12 col-md-6">
                            <div className="form-group">
                                    <label htmlFor="name">Cpf</label>
                                    <input type="text" className="form-control" 
                                        name="cpf" 
                                        value={this.state.user.IdClienteCpf}
                                        onChange={e => this.updatefield(e)}
                                        placeholder="Digite o cpf.."
                                        />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="name">Nome</label>
                                    <input type="text" className="form-control" 
                                        name="name" 
                                        value={this.state.user.NomeCliente}
                                        onChange={e => this.updatefield(e)}
                                        placeholder="Digite o nome.."
                                        />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="name">E-mail</label>
                                    <input type="email" className="form-control" 
                                        name="email" 
                                        value={this.state.user.Email}
                                        onChange={e => this.updatefield(e)}
                                        placeholder="Digite o email.."
                                        />
                                </div>
                            </div>
                        </div>                          
                        <hr />
                        <div className="row">
                            <div className="col-12 d-flex justify-content end">
                                <button className="btn btn-primary"
                                onClick={e => this.save(e)}>Salvar</button>
                                <button className="btn btn-secondary ml-2" onClick={JwModal.close('jw-modal-cliente')}>Fechar</button>                        
                            </div>
                        </div>
                     </div>    
                </JwModal>   
            </table>
        );
    }
    renderows(){
        /**mapeando usuários que estão no estado do objeto */
        return this.state.list.map((user,index) => {
            return (                                                
                <tr key={index}>
                    <td>{user.idClienteCpf}</td>
                    <td>{user.nomeCliente}</td>
                    <td>{user.email}</td>
                    <td>
                        <button className="btn btn-warning mr-2"
                        onClick={() => this.load(user)}>
                            <i className="fa fa-pencil"></i>
                        </button>
                        <button className="btn btn-danger"
                        onClick={() => this.remove(user)}>
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