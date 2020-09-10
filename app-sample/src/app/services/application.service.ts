
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RepositoryService } from './Repository.service';
import { Application } from '../models/Application';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  uri: string = 'Application/';
  constructor(private _repositoryService: RepositoryService) { }

  getApplications () {
    return this._repositoryService.getData(this.uri);
  }

  createApplications(workDay: Application){
    return this._repositoryService.create(this.uri + 'Create', workDay);
  }

  deleteApplications(id: any){
    return this._repositoryService.delete(this.uri + id);
  }

  getApplicationsById(id: any){
    return this._repositoryService.getData(this.uri + id);
  }

  updateApplications(id: any, workDay: Application){
    return this._repositoryService.update(this.uri + id +'/Update', workDay);
  }
}
