"use strict";(self.webpackChunkcodal_analysis=self.webpackChunkcodal_analysis||[]).push([[296],{8856:(A,m,t)=>{t.r(m),t.d(m,{SubsystemModule:()=>h});var u=t(6895),a=t(1481),e=t(8136),d=t(945);const f=new a.lX("CODAL_ANALYSIS","system","subsystem",d.o);var n=t(1571),v=t(5779),y=t(4934);class i{static#t=this.\u0275fac=function(s){return new(s||i)};static#s=this.\u0275cmp=n.Xpm({type:i,selectors:[["app-subsystem-root"]],features:[n._Bn([a.Ah,{provide:a.rL,useValue:f}])],decls:3,vars:0,template:function(s,S){1&s&&n._UZ(0,"router-outlet")(1,"mts-toast")(2,"mts-image-thumbnail-viewer")},dependencies:[v.q,y.X,e.lC],encapsulation:2})}class c{constructor(o){this.router=o}canActivate(o,s){return!!localStorage.getItem("token")||(this.router.navigate(["/access/signin"]),!1)}static#t=this.\u0275fac=function(s){return new(s||c)(n.LFG(e.F0))};static#s=this.\u0275prov=n.Yz7({token:c,factory:c.\u0275fac,providedIn:"root"})}const p=[{path:"",redirectTo:"/",pathMatch:"full"},{path:"",component:i,children:[{path:"",children:[{path:"access",loadChildren:()=>t.e(840).then(t.bind(t,5840)).then(r=>r.AccessAreaModule)},{path:"",canActivate:[c],loadChildren:()=>t.e(23).then(t.bind(t,23)).then(r=>r.UserAreaModule)}]}]}];class l{static#t=this.\u0275fac=function(s){return new(s||l)};static#s=this.\u0275mod=n.oAB({type:l});static#n=this.\u0275inj=n.cJS({imports:[e.Bz.forChild(p),e.Bz]})}class h{static#t=this.\u0275fac=function(s){return new(s||h)};static#s=this.\u0275mod=n.oAB({type:h});static#n=this.\u0275inj=n.cJS({imports:[u.ez,a.EV,a.Rm,a.G8,l]})}}}]);