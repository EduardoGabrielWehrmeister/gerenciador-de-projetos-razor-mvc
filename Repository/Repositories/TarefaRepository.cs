﻿using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        public SistemaContext context;

        public TarefaRepository()
        {
            context = new SistemaContext();
        }

        public bool Alterar(Tarefa tarefa)
        {
            Tarefa tarefaOriginal = (from x in context.Tarefas where x.Id == x.Id select x).FirstOrDefault();
            if(tarefaOriginal == null)
            {
                return false;
            }
            tarefaOriginal.Titulo = tarefa.Titulo;
            tarefaOriginal.Descricao = tarefa.Descricao;
            tarefaOriginal.Duracao = tarefa.Duracao;
            tarefaOriginal.UsuarioResponsavelId = tarefa.UsuarioResponsavelId;
            //tarefaOriginal.ProjetoId = tarefa.ProjetoId;
            context.SaveChanges();
            return true;
        }

        public bool Apagar(int id)
        {
            Tarefa tarefa = (from x in context.Tarefas where x.Id == id select x).FirstOrDefault();
            if(tarefa == null)
            {
                return false;
            }
            tarefa.RegistroAtivo = false;
            context.SaveChanges();
            return true;
        }

        public int Inserir(Tarefa tarefa)
        {
            tarefa.DataCriacao = DateTime.Now;
            context.Tarefas.Add(tarefa);
            context.SaveChanges();
            return tarefa.Id;
        }

        public Tarefa ObterPeloId(int id)
        {
            return (from x in context.Tarefas where x.Id == id select x).FirstOrDefault();
        }

        public List<Tarefa> ObterTodos(string busca)
        {
            return(from x in context.Tarefas where x.RegistroAtivo == true &&
                   (x.Titulo.Contains(busca) ||
                   x.Categoria.Nome.Contains(busca))
                   orderby x.Titulo
                   select x).ToList();
        }

        public List<Tarefa> ObterTodos(int IdCategoria)
        {
            return context.Tarefas.Include("Categoria").Where(x => x.CategoriaId == IdCategoria).ToList();
        }

        /*public List<Tarefa> ObterTodos(int IdProjeto)
        {
            return context.Tarefas.Include("Projeto").Where(x => x.ProjetoId == IdProjeto).ToList();
        }

        public List<Tarefa> ObterTodos(int IdUsuarioResponsavel)
        {
            return context.Tarefas.Include("UsuarioResponsavel").Where(
                x => x.UsuarioResponsavelId == IdUsuarioResponsavel).ToList();
        }*/
    }
}