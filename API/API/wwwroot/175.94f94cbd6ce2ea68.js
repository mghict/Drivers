"use strict";(self.webpackChunkcodal_analysis=self.webpackChunkcodal_analysis||[]).push([[175],{2175:(b,c,e)=>{e.r(c),e.d(c,{ActivateModule:()=>m});var p=e(6895),h=e(4633),v=e(20),a=e(3959),C=e(9287),f=e(2017),t=e(1571),l=e(7109),s=e(4006),g=e(1260);class r{constructor(u,i,n,o,d){this.router=u,this.toast=i,this.stateService=n,this.security=o,this.identityManager=d,this.minutesToResend=1,this.otp=null,this.mobileNumber="",this.validateOtpDto=new h.Dy,this.mobileNumber=this.stateService.mobileNumber,this.validateOtpDto.mobileNumber=this.stateService.mobileNumber,setTimeout(()=>{this.otpComponent.start()},0)}onSubmit(){this.otp&&(this.validateOtpDto.otp=this.otp,this.security.activate(this.validateOtpDto).subscribe({next:u=>{this.security.setUserToken(u.token),this.identityManager.set(new f.O(u.id,u.token,u.displayName,u.roles)),this.stateService.token=u.token,this.router.navigateByUrl("/"),this.toast.show("\u06a9\u0627\u0631\u0628\u0631 \u06af\u0631\u0627\u0645\u06cc \u060c \u062b\u0628\u062a \u0646\u0627\u0645 \u0628\u0627 \u0645\u0648\u0641\u0642\u06cc\u062a \u0627\u0646\u062c\u0627\u0645 \u0634\u062f.")},error:u=>{this.otp=null}}))}OnReSendOtp(){console.log("s")}static#t=this.\u0275fac=function(i){return new(i||r)(t.Y36(l.F0),t.Y36(a.kl),t.Y36(v.b2),t.Y36(C.I),t.Y36(a.cq))};static#u=this.\u0275cmp=t.Xpm({type:r,selectors:[["moneyon-activate"]],viewQuery:function(i,n){if(1&i&&t.Gf(a.n0,5),2&i){let o;t.iGM(o=t.CRH())&&(n.otpComponent=o.first)}},decls:25,vars:4,consts:[[1,"text-center"],[1,"mt-4",3,"ngSubmit"],["form","ngForm"],[1,"d-flex","justify-content-between","align-items-end","pb-4"],[1,"text-muted"],[1,"pb-1","d-flex"],["routerLink","..",1,"ms-auto"],[1,"fw-bold"],[1,"fa-solid","fa-angle-left"],["name","otp",3,"required","ngModel","reSendOtp","ngModelChange"],[1,"d-grid","mt-4"],["type","submit",1,"btn","btn-primary",3,"disabled"],[1,"text-center","mt-3"],["tabindex","2","routerLink","#","tabindex","6","routerLink","../../signin",1,"text-decoration-none"]],template:function(i,n){if(1&i&&(t.TgZ(0,"h4",0),t._uU(1,"\u062b\u0628\u062a \u0646\u0627\u0645 \u062f\u0631 \u0633\u0627\u0645\u0627\u0646\u0647"),t.qZA(),t.TgZ(2,"form",1,2),t.NdJ("ngSubmit",function(){return n.onSubmit()}),t.TgZ(4,"div",3)(5,"span",4),t._uU(6,"\u06a9\u062f \u062a\u0627\u06cc\u06cc\u062f \u0628\u0631\u0627\u06cc \u0634\u0645\u0627\u0631\u0647 "),t.TgZ(7,"b"),t._uU(8),t.qZA(),t._uU(9," \u067e\u06cc\u0627\u0645\u06a9 \u0634\u062f."),t.qZA()(),t.TgZ(10,"div",5)(11,"small",4),t._uU(12,"\u06a9\u062f \u062a\u0627\u06cc\u06cc\u062f \u0627\u0631\u0633\u0627\u0644 \u0634\u062f\u0647 \u0631\u0627 \u0648\u0627\u0631\u062f \u0646\u0645\u0627\u06cc\u06cc\u062f "),t.qZA(),t.TgZ(13,"a",6)(14,"small",7),t._uU(15," \u06af\u0627\u0645 \u0642\u0628\u0644 "),t._UZ(16,"i",8),t.qZA()()(),t.TgZ(17,"mts-otp-verification",9),t.NdJ("reSendOtp",function(){return n.OnReSendOtp()})("ngModelChange",function(d){return n.otp=d}),t.qZA(),t.TgZ(18,"div",10)(19,"button",11),t._uU(20,"\u0648\u0631\u0648\u062f \u0628\u0647 \u0633\u0627\u0645\u0627\u0646\u0647"),t.qZA()(),t.TgZ(21,"div",12),t._uU(22," \u0622\u06cc\u0627 \u0639\u0636\u0648 \u0633\u0627\u0645\u0627\u0646\u0647 \u0647\u0633\u062a\u06cc\u062f\u061f "),t.TgZ(23,"a",13),t._uU(24,"\u0648\u0631\u0648\u062f \u0628\u0647 \u0633\u0627\u0645\u0627\u0646\u0647"),t.qZA()()()),2&i){const o=t.MAs(3);t.xp6(8),t.Oqu(n.mobileNumber),t.xp6(9),t.Q6J("required",!0)("ngModel",n.otp),t.xp6(2),t.Q6J("disabled",o.invalid)}},dependencies:[s._Y,s.JJ,s.JL,s.Q7,s.On,s.F,g.n,l.rH]})}class m{static#t=this.\u0275fac=function(i){return new(i||m)};static#u=this.\u0275mod=t.oAB({type:m});static#e=this.\u0275inj=t.cJS({imports:[p.ez,s.u5,a.uO,l.Bz.forChild([{path:"",component:r}])]})}}}]);