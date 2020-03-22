create or replace procedure prc_HANDLE_5_REGPAY(on_appcode  out   number ,
                                              oc_error    out   varchar2
) is
/*
   5.����Ĵ�ɷ�  seq_fa01 ��Ϊ 2750000   
   ***** ����Ҫ�� ����� ��ϼ۸� ����
*/
cursor cur_pay is
  select * from newhailin.registerfee r where r.status = '1' /*and guyId = 204893  */ order by settleId;

cursor cur_items(p_settleId number) is
  select * from newhailin.sales s where s.settleid = p_settleId;
c_rc001   varchar2(10);
c_fa001   varchar2(10);
c_rc031   varchar2(3);
c_temp    varchar2(50);
c_sa004   varchar2(10);
c_sa001   varchar2(10);
c_sa003   varchar2(50);
c_rc130   varchar2(10);
c_rc109   varchar2(20);
c_rc400   varchar2(10);
n_regfee1 number;
n_regfee2 number;
nPriceTemp number;
rec_mix   newhailin.mixprice%rowType;
begin
  on_appcode := 1;
  
  for rec_1 in cur_pay loop
    
      --���߱��
      begin
        select rc001,rc130,rc109 into c_rc001,c_rc130,c_rc109
          from rc01
         where rc001 = lpad(to_char(rec_1.guyId),10,'0') ;
      exception
        when no_data_found then
          on_appcode := -2;
          oc_error := '���߱��û�ҵ�!' || rec_1.guyid ;
          return;
        when others then
          on_appcode := -3;
          oc_error := '��ѯ���߱�Ŵ���!' || rec_1.guyid ;
          return;
      end;

      if rec_1.feetype = '4' then  --ԭʼ�շѼ�¼
        c_rc031 := '0'; --ԭʼ
        c_fa001 := pkg_business.fun_EntityPk('FA01');
      else
        c_rc031 := '1'; --����
        c_fa001 := lpad(to_char(rec_1.settleid),10,'0');
      end if;
            
      

      --��ϼ۸���
      if rec_1.mixprice = '1' then
         begin
           select * into rec_mix from newhailin.mixprice where settleId = rec_1.settleid;
         exception
           when no_data_found then
             on_appcode := -100;
             oc_error := '������ϼ۸����!';
             return;
         end;
         
         if mod(rec_mix.oldnums,12) = 0 then
           n_regfee1 := rec_mix.oldprice * (rec_mix.oldnums/12);
         elsif mod(rec_mix.oldnums,6) = 0 then
           n_regfee1 := (rec_mix.oldprice/2) * (rec_mix.oldnums/6);
         else
           nPriceTemp := round(rec_mix.oldprice/12,1);
           n_regfee1 := round(nPriceTemp * rec_mix.oldnums,1);
         end if ;
         
         if mod(rec_mix.newnums,12) = 0 then
           n_regfee2 := rec_mix.newprice * (rec_mix.newnums/12);
         elsif mod(rec_mix.newnums,6) = 0 then
           n_regfee2 := (rec_mix.newprice/2) * (rec_mix.newnums/6);
         else
           nPriceTemp := round(rec_mix.newprice/12,1);
           n_regfee2 := round(nPriceTemp * rec_mix.newnums,1);
         end if ;
         
         if n_regfee1 + n_regfee2 <> rec_1.registerfee then
           on_appcode := -500;
           oc_error := '�Ĵ����������!';
           return;
         end if;
         
         if n_regfee1 > 0 then
             c_rc400 := pkg_business.fun_EntityPk('RC04');
             c_sa001 := pkg_business.fun_EntityPk('SA01');
             insert into rc04
                (rc400,rc001, rc010, rc020, rc022, price, nums, rc030, rc031, rc100, rc200, status)
              values
                (
                    c_rc400,          
                    c_rc001,                                --���߱��
                    c_fa001,                                --������ˮ��
                    rec_1.begintime,                        --��ʼ����
                    to_date('20171231','yyyymmdd'),         --��ֹ����
                    rec_mix.oldprice,                       --����
                    rec_mix.oldnums,                        --�ɷ�����
                    n_regfee1,                              --�Ĵ�ѽ��
                    c_rc031,                                --�ɷ����� 0-ԭʼ�Ǽ� 1-�����ɷ�
                    fun_getOldOperator(rec_1.handler),      --������
                    rec_1.handletime,                       --��������
                    rec_1.status                            --״̬
                );       
                
             insert into sa01
              (sa001, ac001, sa002, sa003, sa004, sa005, price, nums, sa007, sa006, sa008, sa010, status, sa100, sa200,sa020)
                values
                  ( c_sa001,                   --������ˮ��
                    c_rc001,                   --���߱��
                    '08',                      --�������Ʒ���� 08-�ǻҼĴ�
                    '�ǻҼĴ�',                --�������Ʒ����
                    c_rc130,                   --�������Ʒ���(��λ���)
                    '2',                                     --��������
                    rec_mix.oldprice,                        --����
                    rec_mix.oldnums,                         --����
                    n_regfee1,                               --���
                    pkg_business.fun_getBitPrice(c_rc130),   --ԭʼ����
                    '1',                                     --����״̬
                    c_fa001,                                 --������ˮ��
                    rec_1.status,                            --״̬
                    fun_getOldOperator(rec_1.handler),       --������
                    rec_1.handletime,
                    'F'
                  );    
         end if;
         
         
         if n_regfee2 > 0 then 
             c_sa001 := pkg_business.fun_EntityPk('SA01');      
             c_rc400 := pkg_business.fun_EntityPk('RC04');
             insert into rc04
                (rc400,rc001, rc010, rc020, rc022, price, nums, rc030, rc031, rc100, rc200, status)
              values
                (
                    c_rc400,          
                    c_rc001,                                --���߱��
                    c_fa001,                                --������ˮ��
                    to_date('20171231','yyyymmdd'),         --��ʼ����
                    rec_1.endate,                           --��ֹ����
                    rec_mix.newprice,                       --����
                    rec_mix.newnums,                        --�ɷ�����
                    n_regfee2,                              --�Ĵ�ѽ��
                    c_rc031,                                --�ɷ����� 0-ԭʼ�Ǽ� 1-�����ɷ�
                    fun_getOldOperator(rec_1.handler),      --������
                    rec_1.handletime,                       --��������
                    rec_1.status                            --״̬
                );    
             insert into sa01
              (sa001, ac001, sa002, sa003, sa004, sa005, price, nums, sa007, sa006, sa008, sa010, status, sa100, sa200,sa020)
                values
                  ( c_sa001,                   --������ˮ��
                    c_rc001,                   --���߱��
                    '08',                      --�������Ʒ���� 08-�ǻҼĴ�
                    '�ǻҼĴ�',                --�������Ʒ����
                    c_rc130,                   --�������Ʒ���(��λ���)
                    '2',                                     --��������
                    rec_mix.newprice,                        --����
                    rec_mix.newnums,                         --����
                    n_regfee2,                               --���
                    pkg_business.fun_getBitPrice(c_rc130),   --ԭʼ����
                    '1',                                     --����״̬
                    c_fa001,                                 --������ˮ��
                    rec_1.status,                            --״̬
                    fun_getOldOperator(rec_1.handler),       --������
                    rec_1.handletime,
                    'F'
                  );   
         end if;         
      else
        c_rc400 := pkg_business.fun_EntityPk('RC04');
        insert into rc04
            (rc400,rc001, rc010, rc020, rc022, price, nums, rc030, rc031, rc100, rc200, status)
          values
            (
                c_rc400,
                c_rc001,                                --���߱��
                c_fa001,                                --������ˮ��
                rec_1.begintime,                        --��ʼ����
                rec_1.endate,                           --��ֹ����
                rec_1.price,                            --����
                rec_1.paynums,                          --�ɷ�����
                rec_1.price * rec_1.paynums,            --�Ĵ�ѽ��
                c_rc031,                                --�ɷ����� 0-ԭʼ�Ǽ� 1-�����ɷ�
                fun_getOldOperator(rec_1.handler),      --������
                rec_1.handletime,                       --��������
                rec_1.status                            --״̬
            );
         
        --�������ԭʼ�Ǽ�,�������۱�
        if c_rc031 = '1' then
           c_sa001 := pkg_business.fun_EntityPk('SA01');
     
            insert into sa01
              (sa001, ac001, sa002, sa003, sa004, sa005, price, nums, sa007, sa006, sa008, sa010, status, sa100, sa200,sa020)
            values
              ( c_sa001,                   --������ˮ��
                c_rc001,                   --���߱��
                '08',                      --�������Ʒ���� 08-�ǻҼĴ�
                '�ǻҼĴ�',                --�������Ʒ����
                c_rc130,                   --�������Ʒ���(��λ���)
                '2',                                     --��������
                rec_1.price,                             --����
                rec_1.paynums,                           --����
                rec_1.price * rec_1.paynums,             --���
                pkg_business.fun_getBitPrice(c_rc130),   --ԭʼ����
                '1',                                     --����״̬
                c_fa001,                                 --������ˮ��
                rec_1.status,                            --״̬
                fun_getOldOperator(rec_1.handler),       --������
                rec_1.handletime,
                'F'
              );
        end if; 
      end if;
      
      
   
    --����Ĵ渽Ʒ
    if rec_1.attachfee > 0 and (rec_1.settleid >1) then

      for rec_item in cur_items(rec_1.settleid) loop

          begin
           select commodityName into c_temp
             from newhailin.commodity v
            where v.commodityid = rec_item.salesitemid;

           begin
             select item_id into c_sa004
               from v_allItem
              where item_type = '13' and
                    item_text = c_temp and
                    rownum < 2;
           exception
             when no_data_found then
               on_appcode := -4;
               oc_error := '��Ʒû�ҵ�!' || rec_item.salesitemid;
               return;
           end;

           c_sa003 := c_temp;

         exception
           when no_data_found then
             on_appcode := -3;
             oc_error := '��Ʒû�ҵ�!' || '���߱��:' || rec_1.guyid;
             return;
         end;

         c_sa001 := pkg_business.fun_EntityPk('SA01');

         
          --�������۱�
          insert into sa01
            (sa001, ac001, sa002, sa003, sa004, sa005, price, nums, sa007, sa006, sa008, sa010, sa100, sa200, status,sa020)
          values
            ( c_sa001,
              c_rc001,
              '13',     --��Ʒ
              c_sa003,
              c_sa004,
              '2',
              rec_item.fixprice,
              rec_item.nums,
              rec_item.fixprice * rec_item.nums,
              rec_item.fixprice,
              '1',
              c_fa001,
              fun_getOldOperator(rec_item.handler),
              rec_item.handletime,
              rec_item.status,
              'F'
            );
      end loop;

    end if;

  end loop;



exception
  when others then
    on_appcode := -1;
    oc_error := sqlerrm;
end prc_HANDLE_5_REGPAY;
/
