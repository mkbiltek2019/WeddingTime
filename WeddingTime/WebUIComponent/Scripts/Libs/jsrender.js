﻿/*! JsRender v1.0.0-beta: http://github.com/BorisMoore/jsrender and http://jsviews.com/jsviews
informal pre V1.0 commit counter: 47 */
(function (n, t, i) { "use strict"; function it(n, t) { t && t.onError && t.onError(n) === !1 || (this.name = "JsRender Error", this.message = n || "JsRender error") } function o(n, t) { var i; n = n || {}; for (i in t) n[i] = t[i]; return n } function ct(n, t, i) { return (!w.rTag || n) && (v = n ? n.charAt(0) : v, y = n ? n.charAt(1) : y, f = t ? t.charAt(0) : f, c = t ? t.charAt(1) : c, k = i || k, n = "\\" + v + "(\\" + k + ")?\\" + y, t = "\\" + f + "\\" + c, a = "(?:(?:(\\w+(?=[\\/\\s\\" + f + "]))|(?:(\\w+)?(:)|(>)|!--((?:[^-]|-(?!-))*)--|(\\*)))\\s*((?:[^\\" + f + "]|\\" + f + "(?!\\" + c + "))*?)", w.rTag = a + ")", a = new RegExp(n + a + "(\\/)?|(?:\\/(\\w+)))" + t, "g"), et = new RegExp("<.*>|([^\\\\]|^)[{}]|" + n + ".*" + t)), [v, y, f, c, k] } function si(n, t) { t || (t = n, n = i); var e, f, o, u, r = this, s = !t || t === "root"; if (n) { if (u = r.type === t ? r : i, !u) if (e = r.views, r._.useKey) { for (f in e) if (u = e[f].get(n, t)) break } else for (f = 0, o = e.length; !u && f < o; f++) u = e[f].get(n, t) } else if (s) while (r.parent.parent) u = r = r.parent; else while (r && !u) u = r.type === t ? r : i, r = r.parent; return u } function lt() { var n = this.get("item"); return n ? n.index : i } function at() { return this.index } function hi(n) { var r, u = this, f = u.linkCtx, t = (u.ctx || {})[n]; return t === i && f && f.ctx && (t = f.ctx[n]), t === i && (t = wi[n]), t && typeof t == "function" && (r = function () { return t.apply(this || u, arguments) }, o(r, t)), r || t } function ci(n, t, u) { var c, f, s, e = +u === u && u, o = t.linkCtx; return e && (u = (e = t.tmpl.bnds[e - 1])(t.data, t, r)), s = u.args[0], (n || e) && (f = o && o.tag || { _: { inline: !o, bnd: e }, tagName: n + ":", flow: !0, _is: "tag" }, o && (o.tag = f, f.linkCtx = f.linkCtx || o, u.ctx = h(u.ctx, o.view.ctx)), f.tagCtx = u, u.view = t, f.ctx = u.ctx || {}, delete u.ctx, t._.tag = f, n = n !== "true" && n, n && ((c = t.getRsc("converters", n)) || l("Unknown converter: {{" + n + ":")) && (f.depends = c.depends, s = c.apply(f, u.args)), s = e && t._.onRender ? t._.onRender(s, t, e) : s, t._.tag = i), s } function li(n, t) { for (var f, e, u = this; f === i && u;) e = u.tmpl[n], f = e && e[t], u = u.parent; return f || r[n][t] } function ai(n, t, u, f, s) { var et, c, ot, it, g, v, ut, p, a, nt, k, st, w, ft, b = "", tt = +f === f && f, y = t.linkCtx || 0, d = t.ctx, ht = u || t.tmpl, ct = t._; for (n._is === "tag" && (c = n, n = c.tagName), tt && (f = (st = ht.bnds[tt - 1])(t.data, t, r)), ut = f.length, c = c || y.tag, v = 0; v < ut; v++) a = f[v], k = a.tmpl, k = a.content = k && ht.tmpls[k - 1], u = a.props.tmpl, v || u && c || (w = t.getRsc("tags", n) || l("Unknown tag: {{" + n + "}}")), u = u || (c ? c : w).template || k, u = "" + u === u ? t.getRsc("templates", u) || e(u) : u, o(a, { tmpl: u, render: rt, index: v, view: t, ctx: h(a.ctx, d) }), c || (w._ctr ? (c = new w._ctr, ft = !!c.init, c.attr = c.attr || w.attr || i) : c = { render: w.render }, c._ = { inline: !y }, y && (y.attr = c.attr = y.attr || c.attr, y.tag = c, c.linkCtx = y), (c._.bnd = st || y.fn) ? c._.arrVws = {} : c.dataBoundOnly && l("{^{" + n + "}} tag must be data-bound"), c.tagName = n, c.parent = g = d && d.tag, c._is = "tag", c._def = w), ct.tag = c, a.tag = c, c.tagCtxs = f, c.flow || (nt = a.ctx = a.ctx || {}, ot = c.parents = nt.parentTags = d && h(nt.parentTags, d.parentTags) || {}, g && (ot[g.tagName] = g), nt.tag = c); for (c.rendering = {}, v = 0; v < ut; v++) a = c.tagCtx = f[v], c.ctx = a.ctx, !v && ft && (c.init(a, y, c.ctx), ft = i), p = i, (et = c.render) && (p = et.apply(c, a.args)), p = p !== i ? p : a.tmpl && a.render() || (s ? i : ""), b = b ? b + (p || "") : p; return delete c.rendering, c.tagCtx = c.tagCtxs[0], c.ctx = c.tagCtx.ctx, c._.inline && (it = c.attr) && it !== "html" && (b = it === "text" ? bt.html(b) : ""), tt && t._.onRender ? t._.onRender(b, t, tt) : b } function d(n, t, i, r, u, f, e, o) { var a, h, c, v = t === "array", l = { key: 0, useKey: v ? 0 : 1, id: "" + oi++, onRender: o, bnds: {} }, s = { data: r, tmpl: u, content: e, views: v ? [] : {}, parent: i, type: t, get: si, getIndex: at, getRsc: li, hlp: hi, _: l, _is: "view" }; return i ? (a = i.views, h = i._, h.useKey ? (a[l.key = "_" + h.useKey++] = s, s.index = b.debugMode ? ki : "", s.getIndex = lt, c = h.tag, l.bnd = v && (!c || !!c._.bnd && c)) : a.splice(l.key = s.index = f, 0, s), s.ctx = n || i.ctx) : s.ctx = n, s } function vi(n) { var t, i, r, u, f; for (t in p) if (u = p[t], (f = u.compile) && (i = n[t + "s"])) for (r in i) i[r] = f(r, i[r], n, t, u) } function yi(n, t, i) { var u, r; return typeof t == "function" ? t = { depends: t.depends, render: t } : ((r = t.template) && (t.template = "" + r === r ? e[r] || e(r) : r), t.init !== !1 && (u = t._ctr = function () { }, (u.prototype = t).constructor = u)), i && (t._parentTmpl = i), t } function vt(r, u, f, o, s, c) { function v(i) { if ("" + i === i || i.nodeType > 0) { try { a = i.nodeType > 0 ? i : !et.test(i) && t && t(n.document).find(i)[0] } catch (u) { } return a && (i = a.getAttribute(ht), r = r || i, i = e[i], i || (r = r || "_" + ei++, a.setAttribute(ht, r), i = e[r] = vt(r, a.innerHTML, f, o, s, c))), i } } var l, a; return u = u || "", l = v(u), c = c || (u.markup ? u : {}), c.tmplName = r, f && (c._parentTmpl = f), !l && u.markup && (l = v(u.markup)) && l.fn && (l.debug !== u.debug || l.allowCode !== u.allowCode) && (l = l.markup), l !== i ? (r && !f && (tt[r] = function () { return u.render.apply(u, arguments) }), l.fn || u.fn ? l.fn && (u = r && r !== l.tmplName ? h(c, l) : l) : (u = yt(l, c), ut(l.replace(ti, "\\$&"), u)), vi(c), u) : void 0 } function yt(n, t) { var i, f = b.wrapMap || {}, r = o({ markup: n, tmpls: [], links: {}, tags: {}, bnds: [], _is: "template", render: rt }, t); return t.htmlTag || (i = ui.exec(n), r.htmlTag = i ? i[1].toLowerCase() : ""), i = f[r.htmlTag], i && i !== f.div && (r.markup = u.trim(r.markup)), r } function pi(n, t) { function u(e, o, s) { var l, h, a, c; if (e && "" + e !== e && !e.nodeType && !e.markup) { for (a in e) u(a, e[a], o); return r } return o === i && (o = e, e = i), e && "" + e !== e && (s = o, o = e, e = i), c = s ? s[f] = s[f] || {} : u, h = t.compile, (l = w.onBeforeStoreItem) && (h = l(c, e, o, h) || h), e ? o === null ? delete c[e] : c[e] = h ? o = h(e, o, s, n, t) : o : o = h(i, o), h && o && (o._is = n), (l = w.onStoreItem) && l(c, e, o, h), o } var f = n + "s"; r[f] = u; p[n] = t } function rt(n, t, f, o, s, c) { var w, ut, nt, y, tt, it, rt, b, p, ft, k, et, a, v = this, ot = !v.attr || v.attr === "html", g = ""; if (o === !0 && (rt = !0, o = 0), v.tag ? (b = v, v = v.tag, ft = v._, et = v.tagName, a = b.tmpl, t = h(t, v.ctx), p = b.content, b.props.link === !1 && (t = t || {}, t.link = !1), f = f || b.view, n = n === i ? f : n) : a = v.jquery && (v[0] || l('Unknown template: "' + v.selector + '"')) || v, a && (!f && n && n._is === "view" && (f = n), f && (p = p || f.content, c = c || f._.onRender, n === f && (n = f.data, s = !0), t = h(t, f.ctx)), f && f.data !== i || ((t = t || {}).root = n), a.fn || (a = e[a] || e(a)), a)) { if (c = (t && t.link) !== !1 && ot && c, k = c, c === !0 && (k = i, c = f._.onRender), t = a.helpers ? h(a.helpers, t) : t, u.isArray(n) && !s) for (y = rt ? f : o !== i && f || d(t, "array", f, n, a, o, p, c), w = 0, ut = n.length; w < ut; w++) nt = n[w], tt = d(t, "item", y, nt, a, (o || 0) + w, p, c), it = a.fn(nt, tt, r), g += y._.onRender ? y._.onRender(it, tt) : it; else y = rt ? f : d(t, et || "data", f, n, a, o, p, c), ft && !v.flow && (y.tag = v), g += a.fn(n, y, r); return k ? k(g, y) : g } return "" } function l(n) { throw new w.Error(n); } function s(n) { l("Syntax error\n" + n) } function ut(n, t, i, r) { function v(t) { t -= f; t && h.push(n.substr(f, t).replace(nt, "\\n")) } function c(t) { t && s('Unmatched or missing tag: "{{/' + t + '}}" in template:\n' + n) } function y(e, a, y, w, b, k, d, g, tt, it, rt, ut) { k && (b = ":", w = "html"); it = it || i; var at, st, ht = a && [], ot = "", et = "", ct = "", lt = !it && !b && !d; y = y || b; v(ut); f = ut + e.length; g ? p && h.push(["*", "\n" + tt.replace(ni, "$1") + "\n"]) : y ? (y === "else" && (ri.test(tt) && s('for "{{else if expr}}" use "{{else expr}}"'), ht = u[6], u[7] = n.substring(u[7], ut), u = o.pop(), h = u[3], lt = !0), tt && (tt = tt.replace(nt, " "), ot = ft(tt, ht, t).replace(ii, function (n, t, i) { return t ? ct += i + "," : et += i + ",", "" })), et = et.slice(0, -1), ot = ot.slice(0, -1), at = et && et.indexOf("noerror:true") + 1 && et || "", l = [y, w || !!r || "", ot, lt && [], 'params:"' + tt + '",props:{' + et + "}" + (ct ? ",ctx:{" + ct.slice(0, -1) + "}" : ""), at, ht || 0], h.push(l), lt && (o.push(u), u = l, u[7] = f)) : rt && (st = u[0], c(rt !== st && st !== "else" && rt), u[7] = n.substring(u[7], ut), u = o.pop()); c(!u && rt); h = u[3] } var l, p = t && t.allowCode, e = [], f = 0, o = [], h = e, u = [, , , e]; return c(o[0] && o[0][3].pop()[0]), n.replace(a, y), v(n.length), (f = e[e.length - 1]) && c("" + f !== f && +f[7] === f[7] && f[0]), pt(e, i ? n : t, i) } function pt(n, i, r) { var c, f, e, l, a, y, st, ht, ct, lt, ft, p, o, et, v, tt, w, it, at, k, vt, wt, ot, rt, d, h = 0, u = "", g = "", ut = {}, bt = n.length; for ("" + i === i ? (v = r ? 'data-link="' + i.replace(nt, " ").slice(1, -1) + '"' : i, i = 0) : (v = i.tmplName || "unnamed", i.allowCode && (ut.allowCode = !0), i.debug && (ut.debug = !0), p = i.bnds, et = i.tmpls), c = 0; c < bt; c++) if (f = n[c], "" + f === f) u += '\nret+="' + f + '";'; else if (e = f[0], e === "*") u += "" + f[1]; else { if (l = f[1], a = f[2], it = f[3], y = f[4], g = f[5], at = f[7], (wt = e === "else") || (h = 0, p && (o = f[6]) && (h = p.push(o))), (ot = e === ":") ? (l && (e = l === "html" ? ">" : l + e), g && (rt = "prm" + c, g = "try{var " + rt + "=[" + a + "][0];}catch(e){" + rt + '="";}\n', a = rt)) : (it && (tt = yt(at, ut), tt.tmplName = v + "/" + e, pt(it, tt), et.push(tt)), wt || (w = e, vt = u, u = ""), k = n[c + 1], k = k && k[0] === "else"), y += ",args:[" + a + "]}", ot && o || l && e !== ">") { if (d = new Function("data,view,j,u", " // " + v + " " + h + " " + e + "\n" + g + "return {" + y + ";"), d.paths = o, d._ctxs = e, r) return d; ft = 1 } if (u += ot ? "\n" + (o ? "" : g) + (r ? "return " : "ret+=") + (ft ? (ft = 0, lt = !0, 'c("' + l + '",view,' + (o ? (p[h - 1] = d, h) : "{" + y) + ");") : e === ">" ? (ht = !0, "h(" + a + ");") : (ct = !0, "(v=" + a + ")!=" + (r ? "=" : "") + 'u?v:"";')) : (st = !0, "{view:view,tmpl:" + (it ? et.length : "0") + "," + y + ","), w && !k) { if (u = "[" + u.slice(0, -1) + "]", (r || o) && (u = new Function("data,view,j,u", " // " + v + " " + h + " " + w + "\nreturn " + u + ";"), o && ((p[h - 1] = u).paths = o), u._ctxs = e, r)) return u; u = vt + '\nret+=t("' + w + '",view,this,' + (h || u) + ");"; o = 0; w = 0 } } u = "// " + v + "\nvar j=j||" + (t ? "jQuery." : "js") + "views" + (ct ? ",v" : "") + (st ? ",t=j._tag" : "") + (lt ? ",c=j._cnvt" : "") + (ht ? ",h=j.converters.html" : "") + (r ? ";\n" : ',ret="";\n') + (b.tryCatch ? "try{\n" : "") + (ut.debug ? "debugger;" : "") + u + (r ? "\n" : "\nreturn ret;\n") + (b.tryCatch ? "\n}catch(e){return j._err(e);}" : ""); try { u = new Function("data,view,j,u", u) } catch (kt) { s("Compiled template code:\n\n" + u, kt) } return i && (i.fn = u), u } function ft(n, t, i) { function b(b, k, d, g, nt, tt, it, rt, et, ot, st, ht, ct, lt, at, vt, yt, pt, wt, bt) { function gt(n, i, r, f, o, s, h, c) { if (r && (t && (u === "linkTo" && (e = t._jsvto = t._jsvto || [], e.push(nt)), (!u || l) && t.push(nt.slice(i.length))), r !== ".")) { var a = (f ? 'view.hlp("' + f + '")' : o ? "view" : "data") + (c ? (s ? "." + s : f ? "" : o ? "" : "." + r) + (h || "") : (c = f ? "" : o ? s || "" : r, "")); return a = a + (c ? "." + c : ""), i + (a.slice(0, 9) === "view.data" ? a.slice(5) : a) } return n } var kt; if (tt = tt || "", d = d || k || ht, nt = nt || et, ot = ot || yt || "", it) s(n); else return t && vt && !h && !o && (!u || l || e) && (kt = p[r], bt.length - 1 > wt - kt && (kt = bt.slice(kt, wt + 1), vt = y + ":" + kt + f, vt = w[vt] = w[vt] || ut(v + vt + c, i, !0), vt.paths || ft(kt, vt.paths = [], i), (e || t).push({ _jsvOb: vt }))), h ? (h = !ct, h ? b : '"') : o ? (o = !lt, o ? b : '"') : (d ? (r++, p[r] = wt++, d) : "") + (pt ? r ? "" : u ? (u = l = e = !1, "\b") : "," : rt ? (r && s(n), u = nt, l = g, "\b" + nt + ":") : nt ? nt.split("^").join(".").replace(dt, gt) + (ot ? (a[++r] = !0, nt.charAt(0) !== "." && (p[r] = wt), ot) : tt) : tt ? tt : at ? (a[r--] = !1, at) + (ot ? (a[++r] = !0, ot) : "") : st ? (a[r] || s(n), ",") : k ? "" : (h = ct, o = lt, '"')) } var u, e, l, w = i.links, a = {}, p = { 0: -1 }, r = 0, o = !1, h = !1; return (n + " ").replace(/\)\^/g, ").").replace(gt, b) } function h(n, t) { return n && n !== t ? t ? o(o({}, t), n) : n : t && o({}, t) } function wt(n) { return st[n] || (st[n] = "&#" + n.charCodeAt(0) + ";") } function kt(n) { var t = this, f = t.tagCtx, e = !arguments.length, r = "", o = e || 0; return t.rendering.done || (e ? r = i : n !== i && (n = t.prep ? t.prep(n) : n, r += f.render(n), o += u.isArray(n) ? n.length : 1), (t.rendering.done = o) && (t.selected = f.index)), r } if ((!t || !t.views) && !n.jsviews) { var u, g, a, et, v = "{", y = "{", f = "}", c = "}", k = "^", dt = /^(!*?)(?:null|true|false|\d[\d.]*|([\w$]+|\.|~([\w$]+)|#(view|([\w$]+))?)([\w$.^]*?)(?:[.[^]([\w$]+)\]?)?)$/g, gt = /(\()(?=\s*\()|(?:([([])\s*)?(?:(\^?)(!*?[#~]?[\w$.^]+)?\s*((\+\+|--)|\+|-|&&|\|\||===|!==|==|!=|<=|>=|[<>%*:?\/]|(=))\s*|(!*?[#~]?[\w$.^]+)([([])?)|(,\s*)|(\(?)\\?(?:(')|("))|(?:\s*(([)\]])(?=\s*\.|\s*\^|\s*$)|[)\]])([([]?))|(\s+)/g, nt = /[ \t]*(\r\n|\n|\r)/g, ni = /\\(['"])/g, ti = /['"\\]/g, ii = /\x08(~)?([^\x08]+)\x08/g, ri = /^if\s/, ui = /<(\w+)[>\s]/, ot = /[\x00`><"'&]/g, fi = ot, ei = 0, oi = 0, st = { "&": "&amp;", "<": "&lt;", ">": "&gt;", "\x00": "&#0;", "'": "&#39;", '"': "&#34;", "`": "&#96;" }, ht = "data-jsv-tmpl", tt = {}, p = { template: { compile: vt }, tag: { compile: yi }, helper: {}, converter: {} }, r = { jsviews: "v1.0.0-beta", render: tt, settings: { delimiters: ct, debugMode: !0, tryCatch: !0 }, sub: { View: d, Error: it, tmplFn: ut, parse: ft, extend: o, error: l, syntaxError: s }, _cnvt: ci, _tag: ai, _err: function (n) { return b.debugMode ? "Error: " + (n.message || n) + ". " : "" } }; (it.prototype = new Error).constructor = it; lt.depends = function () { return [this.get("item"), "index"] }; at.depends = function () { return ["index"] }; for (g in p) pi(g, p[g]); var e = r.templates, bt = r.converters, wi = r.helpers, bi = r.tags, w = r.sub, b = r.settings, ki = "Error: #index in nested view: use #getIndex()"; t ? (u = t, u.fn.render = rt) : (u = n.jsviews = {}, u.isArray = Array && Array.isArray || function (n) { return Object.prototype.toString.call(n) === "[object Array]" }); u.render = tt; u.views = r; u.templates = e = r.templates; bi({ "else": function () { }, "if": { render: function (n) { var t = this; return t.rendering.done || !n && (arguments.length || !t.tagCtx.index) ? "" : (t.rendering.done = !0, t.selected = t.tagCtx.index, t.tagCtx.render()) }, onUpdate: function (n, t, i) { for (var r, f, u = 0; (r = this.tagCtxs[u]) && r.args.length; u++) if (r = r.args[0], f = !r != !i[u].args[0], !!r || f) return f; return !1 }, flow: !0 }, "for": { render: kt, onArrayChange: function (n, t) { var i, u = this, r = t.change; if (this.tagCtxs[1] && (r === "insert" && n.target.length === t.items.length || r === "remove" && !n.target.length || r === "refresh" && !t.oldItems.length != !n.target.length)) this.refresh(); else for (i in u._.arrVws) i = u._.arrVws[i], i.data === n.target && i._.onArrayChange.apply(i, arguments); n.done = !0 }, flow: !0 }, props: { prep: function (n) { var t, i = []; for (t in n) i.push({ key: t, prop: n[t] }); return i }, render: kt, flow: !0 }, include: { flow: !0 }, "*": { render: function (n) { return n }, flow: !0 } }); bt({ html: function (n) { return n != i ? String(n).replace(fi, wt) : "" }, attr: function (n) { return n != i ? String(n).replace(ot, wt) : n === null ? n : "" }, url: function (n) { return n != i ? encodeURI(String(n)) : n === null ? n : "" } }); ct() } })(this, this.jQuery);
/*
//# sourceMappingURL=jsrender.min.js.map
*/